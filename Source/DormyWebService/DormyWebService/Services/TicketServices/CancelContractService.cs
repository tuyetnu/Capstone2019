using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.ContractEntities;
using DormyWebService.Entities.TicketEntities;
using DormyWebService.Repositories;
using DormyWebService.Services.ParamServices;
using DormyWebService.Services.UserServices;
using DormyWebService.Utilities;
using DormyWebService.ViewModels.TicketViewModels.CancelContract;
using DormyWebService.ViewModels.TicketViewModels.CancelContract.ApproveCancelContract;
using DormyWebService.ViewModels.TicketViewModels.CancelContract.GetCancelContract;
using DormyWebService.ViewModels.TicketViewModels.CancelContract.RejectCancelContract;
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
            //throw new NotImplementedException();
            var sieveModel = new SieveModel()
            {
                PageSize = pageSize,
                Sorts = sorts,
                Page = page,
                Filters = filters
            };
            var resultResponses = _repoWrapper.CancelContract.GetAllIncludingResponse();
            var result = _sieveProcessor.Apply(sieveModel, resultResponses.AsQueryable(), applyPagination: false).ToList();

            var response = new AdvancedGetCancelContractResponse()
            {
                CurrentPage = page ?? 1,
                TotalPage = (int)Math.Ceiling((double)result.Count / pageSize ?? 1),
                ResultList = _sieveProcessor
                    .Apply(sieveModel, result.AsQueryable(), applyFiltering: false, applySorting: false).ToList()
            };
            return response;
        }

        public async Task<ActionResult<ApproveCancelContractResponse>> ApproveContractCancel(ApproveCancelContractRequest approveCancel)
        {
            var cancelContract = await _repoWrapper.CancelContract.FindByIdAsync(approveCancel.cancelContractFormId);
            if (cancelContract == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "ApproveContractCancel: Contract cancel form not found");
            }
            if (cancelContract.Status != RequestStatus.Pending)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "ApproveContractCancel: Contract cancel form status is not Pending");
            }
            var student = await _repoWrapper.Student.FindByIdAsync(cancelContract.StudentId);
            if (student == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "ApproveContractCancel: Student not found");
            }
            var now = DateTime.Now.AddHours(GlobalParams.TimeZone);
            
            var contract = await _repoWrapper.Contract.FindAsync(s => s.StudentId == student.StudentId && s.Status == ContractStatus.Active);
            if (contract == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "ApproveContractCancel: Contract not found");
            }
            cancelContract.Status = RequestStatus.Approved;
            cancelContract.LastUpdated = now;
            cancelContract.StaffId = approveCancel.staffId;
            await _repoWrapper.CancelContract.UpdateAsync(cancelContract, cancelContract.CancelContractFormId);

            contract.EndDate = cancelContract.CancelationDate;
            contract.LastUpdate = now;
            await _repoWrapper.Contract.UpdateAsync(contract, contract.ContractId);

            return new ApproveCancelContractResponse(cancelContract.CancelContractFormId);
        }

        public async Task<ActionResult<GetCancelContractDetail>> GetCancelContractDetail(int id)
        {
            var cancelContract = await _repoWrapper.CancelContract.FindByIdAsync(id);
            if(cancelContract == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "CancelContract: Renew contract request not found");
            }
            return new GetCancelContractDetail(cancelContract);
        }

        public async Task<ActionResult<RejectCancelContractRespone>> RejectCancelContract(RejectCancelContractRequest rejectCancel)
        {
            var cancelContract = await _repoWrapper.CancelContract.FindByIdAsync(rejectCancel.CancelContractFormId);
            if (cancelContract == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "RejectContractCancel: Contract cancel form not found");
            }
            if (cancelContract.Status != RequestStatus.Pending)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "RejectContractCancel: Contract cancel form status is not Pending");
            }
            var student = await _repoWrapper.Student.FindByIdAsync(cancelContract.StudentId);
            if (student == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "RejectContractCancel: Student not found");
            }
            var now = DateTime.Now.AddHours(GlobalParams.TimeZone);

            var contract = await _repoWrapper.Contract.FindAsync(s => s.StudentId == student.StudentId && s.Status == ContractStatus.Active);
            if (contract == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "RejectContractCancel: Contract not found");
            }
            cancelContract.Status = RequestStatus.Rejected;
            cancelContract.LastUpdated = now;
            cancelContract.StaffId = rejectCancel.StaffId;
            cancelContract.Reason = rejectCancel.Reason;


            await _repoWrapper.CancelContract.UpdateAsync(cancelContract, cancelContract.CancelContractFormId);

            return new RejectCancelContractRespone(cancelContract.CancelContractFormId);

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