using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.ContractEntities;
using DormyWebService.Entities.RoomEntities;
using DormyWebService.Repositories;
using DormyWebService.Services.ParamServices;
using DormyWebService.Services.UserServices;
using DormyWebService.Utilities;
using DormyWebService.ViewModels.EquipmentViewModels.GetEquipment;
using DormyWebService.ViewModels.TicketViewModels.RenewContractRequestViewModels.GetRenewContract;
using DormyWebService.ViewModels.TicketViewModels.RenewContractRequestViewModels.SendRenewContractRequest;
using Sieve.Models;
using Sieve.Services;

namespace DormyWebService.Services.TicketServices
{
    public class RenewContractService : IRenewContractService
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly IStudentService _studentService;
        private readonly ISieveProcessor _sieveProcessor;
        private readonly IParamService _paramService;
        private readonly IStaffService _staffService;

        public RenewContractService(IRepositoryWrapper repoWrapper, IStudentService studentService, ISieveProcessor sieveProcessor, IParamService paramService, IStaffService staffService)
        {
            _repoWrapper = repoWrapper;
            _studentService = studentService;
            _sieveProcessor = sieveProcessor;
            _paramService = paramService;
            _staffService = staffService;
        }

        public async Task<SendRenewContractRequestResponse> SendRenewContract(SendRenewContractRequestRequest request)
        {
            var acceptableMonths = (await _paramService.FindAllByParamType(GlobalParams.ParamTypeContractParam)).Select(p=>p.Value).ToList();
            if (!acceptableMonths.Contains(request.Month))
            {
                throw new HttpStatusCodeException(HttpStatusCode.Forbidden, "RenewContractService: Month is invalid");
            }

            var student = await _studentService.FindById(request.StudentId);

            //Check if student has room
            if (student.RoomId == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.Forbidden, "RenewContractService: Student doesn't have room");
            }

            //Check student's evaluation point is enough
            var contractRenewalEvaluationScoreMargin = (await 
                _paramService.FindById(GlobalParams.ParamContractRenewalEvaluationScoreMargin))?.Value ?? GlobalParams.DefaultContractRenewalEvaluationPointMargin;

            if (student.EvaluationScore < contractRenewalEvaluationScoreMargin)
            {
                throw new HttpStatusCodeException(HttpStatusCode.Forbidden, "RenewContractService: Student's evaluation is not enough");
            }

            var contracts =(List<Contract>)
                await _repoWrapper.Contract.FindAllAsyncWithCondition(c => c.StudentId == student.StudentId && c.Status == ContractStatus.Active);
            if (contracts == null || !contracts.Any())
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "RenewContractService: Student doesn't have any active contract'");
            }

            var renewContract = SendRenewContractRequestRequest.EntityFromRequest(request, contracts[0]);

            renewContract = await _repoWrapper.RenewContract.CreateAsync(renewContract);

            return SendRenewContractRequestResponse.ResponseFromEntity(renewContract);
        }

        public async Task<AdvancedGetRenewContractResponse> AdvancedGetRenewContract(string sorts, string filters, int? page, int? pageSize)
        {
            var sieveModel = new SieveModel()
            {
                PageSize = pageSize,
                Sorts = sorts,
                Page = page,
                Filters = filters
            };

            var renewContract = await _repoWrapper.RenewContract.FindAllAsync();

            if (renewContract == null || renewContract.Any() == false)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "RenewContractService: No contract is found");
            }

            var resultResponses = new List<GetRenewContractResponse>();

            foreach (var contractRenewalForm in renewContract)
            {
                var student = await _studentService.FindById(contractRenewalForm.StudentId);
                Staff staff = null;
                if (contractRenewalForm.StaffId != null)
                {
                    staff = await _staffService.FindById(contractRenewalForm.StaffId.Value);
                }

                resultResponses.Add(GetRenewContractResponse.ResponseFromEntity(contractRenewalForm, student, staff));
            }

            //Apply filter, sort
            var result = _sieveProcessor.Apply(sieveModel, resultResponses.AsQueryable(), applyPagination: false).ToList();

            var response = new AdvancedGetRenewContractResponse()
            {
                CurrentPage = page ?? 1,
                TotalPage = (int)Math.Ceiling((double)result.Count / pageSize ?? 1),
                //Apply pagination
                ResultList = _sieveProcessor
                    .Apply(sieveModel, result.AsQueryable(), applyFiltering: false, applySorting: false).ToList()
            };

            //Return List of result
            return response;
        }
    }
}
