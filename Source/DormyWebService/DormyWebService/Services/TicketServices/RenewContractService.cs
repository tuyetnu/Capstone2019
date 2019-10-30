using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DormyWebService.Entities.ContractEntities;
using DormyWebService.Repositories;
using DormyWebService.Services.ParamServices;
using DormyWebService.Services.UserServices;
using DormyWebService.Utilities;
using DormyWebService.ViewModels.TicketViewModels.RenewContractRequestViewModels.SendRenewContractRequest;
using Sieve.Services;

namespace DormyWebService.Services.TicketServices
{
    public class RenewContractService : IRenewContractService
    {
        private IRepositoryWrapper _repoWrapper;
        private readonly IStudentService _studentService;
        private ISieveProcessor _sieveProcessor;
        private IParamService _paramService;

        public RenewContractService(IRepositoryWrapper repoWrapper, IStudentService studentService, ISieveProcessor sieveProcessor, IParamService paramService)
        {
            _repoWrapper = repoWrapper;
            _studentService = studentService;
            _sieveProcessor = sieveProcessor;
            _paramService = paramService;
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
    }
}
