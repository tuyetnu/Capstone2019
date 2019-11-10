using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.ContractEntities;
using DormyWebService.Repositories;
using DormyWebService.Services.ParamServices;
using DormyWebService.Services.UserServices;
using DormyWebService.Utilities;
using DormyWebService.ViewModels.TicketViewModels.CancelContract.GetCancelContract;
using DormyWebService.ViewModels.TicketViewModels.CancelContract.SendCancelContractRequest;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;
using Sieve.Services;

namespace DormyWebService.Services.TicketServices
{
    public class CancelContractService : ICancelContractService
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly IStudentService _studentService;
        private readonly ISieveProcessor _sieveProcessor;
        private readonly IParamService _paramService;
        private readonly IStaffService _staffService;

        public CancelContractService(IRepositoryWrapper repoWrapper, IStudentService studentService, ISieveProcessor sieveProcessor, IParamService paramService, IStaffService staffService)
        {
            _repoWrapper = repoWrapper;
            _studentService = studentService;
            _sieveProcessor = sieveProcessor;
            _paramService = paramService;
            _staffService = staffService;
        }

        public async Task<ActionResult<AdvancedGetCancelContractResponse>> AdvancedGetCancelContract(string sorts, string filters, int? page, int? pageSize)
        {
            throw new NotImplementedException();
            //var sieveModel = new SieveModel()
            //{
            //    PageSize = pageSize,
            //    Sorts = sorts,
            //    Page = page,
            //    Filters = filters
            //};
            //
            //var cancelContracts = await _repoWrapper.CancelContract.FindAllAsync();
            //var resultResponses = new List<CancelContractResponse>();
            //foreach (var cancelContractForm in cancelContracts)
            //{
            //    var student = await _studentService.FindById(cancelContractForm.StudentId);
            //    Staff staff = null;
            //    if (cancelContractForm.StaffId != null)
            //    {
            //        staff = await _staffService.FindById(cancelContractForm.StaffId.Value);
            //    }
            //    var contracts = await _repoWrapper.Contract.FindByAsync(c => c.StudentId == student.StudentId && c.Status == ContractStatus.Active);
            //    
            //
            //    resultResponses.Add(CancelContractResponse.ResponseFromEntity(cancelContractForm, student, staff, contract, student.Room));
            //}
        }

        public async Task<ActionResult<SendCancelContractFormResponse>> SendCancelContract(SendCancelContractFormRequest request)
        {
            var student = await _studentService.FindById(request.StudentId);
            if (student.RoomId == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.Forbidden, "CancelContractService: Student doesn't have room");
            }
            var contracts = (List<Contract>)
                await _repoWrapper.Contract.FindAllAsyncWithCondition(c => c.StudentId == student.StudentId && c.Status == ContractStatus.Active);
            if (contracts == null || !contracts.Any())
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "CancelContractService: Student doesn't have any active contract'");
            }
            var cancelContract = SendCancelContractFormRequest.EntityFromRequest(request, contracts[0]);

            cancelContract = await _repoWrapper.CancelContract.CreateAsync(cancelContract);
            return SendCancelContractFormResponse.ResponseFromEntity(cancelContract);


        }
    }
}