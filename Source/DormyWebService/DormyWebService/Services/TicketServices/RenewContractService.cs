using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.ContractEntities;
using DormyWebService.Entities.RoomEntities;
using DormyWebService.Entities.TicketEntities;
using DormyWebService.Repositories;
using DormyWebService.Services.ParamServices;
using DormyWebService.Services.UserServices;
using DormyWebService.Utilities;
using DormyWebService.ViewModels.EquipmentViewModels.GetEquipment;
using DormyWebService.ViewModels.TicketViewModels.RenewContractRequestViewModels.ApproveRenewContract;
using DormyWebService.ViewModels.TicketViewModels.RenewContractRequestViewModels.GetRenewContract;
using DormyWebService.ViewModels.TicketViewModels.RenewContractRequestViewModels.GetRenewContractDetail;
using DormyWebService.ViewModels.TicketViewModels.RenewContractRequestViewModels.RejectRenewContract;
using DormyWebService.ViewModels.TicketViewModels.RenewContractRequestViewModels.SendRenewContractRequest;
using Microsoft.AspNetCore.Mvc;
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
                _paramService.FindById(GlobalParams.ParamContractRenewalEvaluationPointMargin))?.Value ?? GlobalParams.DefaultContractRenewalEvaluationPointMargin;

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

            var resultResponses = _repoWrapper.RenewContract.FindAllIncluding();

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

        public async Task<ActionResult<ApproveRenewContractResponse>> ApproveContractRenewal(ApproveRenewContractRequest approveRenew)
        {
            //throw new NotImplementedException();
            var renewContract = await _repoWrapper.RenewContract.FindByIdAsync(approveRenew.contractId);
            if (renewContract == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "ContractRenewal: Contract renewal form not found");
            }
            if (renewContract.Status != RequestStatus.Pending)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "ContractRenewal: Contract renewal form status is not Pending");
            }
            var student = await _repoWrapper.Student.FindByIdAsync(renewContract.StudentId);
            if (student == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "ContractRenewal: Student not found");
            }
            var user = await _repoWrapper.User.FindByIdAsync(student.StudentId);
            var maxYear = await _paramService.FindById(GlobalParams.ParamMaxYearForStaying);
            if (maxYear?.Value == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.Forbidden, "ContractRenewal: MaxYear is not exist");
            }
            var now = DateTime.Now.AddHours(GlobalParams.TimeZone);
            if ((now.AddMonths(renewContract.Month).Year - student.StartedSchoolYear) > maxYear.Value) 
            {
                throw new HttpStatusCodeException(HttpStatusCode.Forbidden, "ContractRenewal: Student's year stay at university is more than 5");
            }
            var contract = await _repoWrapper.Contract.FindAsync(s => s.StudentId == student.StudentId && s.Status == ContractStatus.Active);
            if (contract == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "ContractRenewal: Contract not found");
            }
            renewContract.Status = RequestStatus.Approved;
            renewContract.LastUpdated = now;
            renewContract.StaffId = approveRenew.staffId;
            await _repoWrapper.RenewContract.UpdateAsync(renewContract, renewContract.ContractRenewalFormId);

            contract.EndDate = contract.EndDate.AddMonths(renewContract.Month);
            contract.LastUpdate = now;

            //send notification

            if (user.IsLoggedIn == true && user.DeviceToken != null && user.DeviceToken.Length > 0)
            {
                string[] deviceTokens = new string[1];
                deviceTokens[0] = user.DeviceToken;
                PushNotificationToFirebase pushNotification = new PushNotificationToFirebase();
                string body = "Yêu cầu gia hạn hợp đồng của bạn đã được duyệt, ngày hết hạn hợp đã được gia hạn đến: "+ contract.EndDate.AddMonths(renewContract.Month).ToString(GlobalParams.BirthDayFormat);
                await pushNotification.PushNotification(deviceTokens, body);
            }

            await _repoWrapper.Contract.UpdateAsync(contract, contract.ContractId);

            return new ApproveRenewContractResponse(renewContract.ContractRenewalFormId);
            
            


        }

        public async Task<ActionResult<RejectRenewContractResponse>> RejectContractRenewal(RejectRenewContractRequest rejectRenew)
        {
            var renewContract = await _repoWrapper.RenewContract.FindByIdAsync(rejectRenew.RenewContractFormId);
            if (renewContract == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "ContractRenewal: Contract renewal form not found");
            }
            if (renewContract.Status != RequestStatus.Pending)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "ContractRenewal: Contract renewal form status is not Pending");
            }
            var student = await _repoWrapper.Student.FindByIdAsync(renewContract.StudentId);
            if (student == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "ContractRenewal: Student not found");
            }
            var user = await _repoWrapper.User.FindByIdAsync(student.StudentId);

            var maxYear = await _paramService.FindById(GlobalParams.ParamMaxYearForStaying);
            if (maxYear?.Value == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.Forbidden, "ContractRenewal: MaxYear is not exist");
            }
            var now = DateTime.Now.AddHours(GlobalParams.TimeZone);
            if ((now.AddMonths(renewContract.Month).Year - student.StartedSchoolYear) > maxYear.Value)
            {
                throw new HttpStatusCodeException(HttpStatusCode.Forbidden, "ContractRenewal: Student's year stay at university is more than 5");
            }
            var contract = await _repoWrapper.Contract.FindAsync(s => s.StudentId == student.StudentId && s.Status == ContractStatus.Active);
            if (contract == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "ContractRenewal: Contract not found");
            }
            renewContract.Status = RequestStatus.Rejected;
            renewContract.LastUpdated = now;
            renewContract.StaffId = rejectRenew.StaffId;
            renewContract.Reason = rejectRenew.Reason;

            //send notification
            if (user.IsLoggedIn == true && user.DeviceToken != null && user.DeviceToken.Length > 0)
            {
                string[] deviceTokens = new string[1];
                deviceTokens[0] = user.DeviceToken;
                PushNotificationToFirebase pushNotification = new PushNotificationToFirebase();
                string body = "Yêu cầu gia hạn hợp đồng của bạn đã bị từ chối vì:" + rejectRenew.Reason;
                await pushNotification.PushNotification(deviceTokens, body);
            }


            await _repoWrapper.RenewContract.UpdateAsync(renewContract, renewContract.ContractRenewalFormId);

            return new RejectRenewContractResponse(renewContract.ContractRenewalFormId);
        }

        public async Task<ActionResult<RenewContractDetailResponse>> GetRenewContractDetail(int id)
        {
            var renewContract = await _repoWrapper.RenewContract.FindByIdAsync(id);
            if (renewContract == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "RenewContract: Renew contract request not found");
            }
            return new RenewContractDetailResponse(renewContract);
        }
    }
}
