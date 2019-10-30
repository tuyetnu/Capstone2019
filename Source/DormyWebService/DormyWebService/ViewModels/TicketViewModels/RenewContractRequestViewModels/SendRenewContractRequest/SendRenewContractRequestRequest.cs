using System;
using System.ComponentModel.DataAnnotations;
using DormyWebService.Entities.ContractEntities;
using DormyWebService.Entities.TicketEntities;
using DormyWebService.Utilities;

namespace DormyWebService.ViewModels.TicketViewModels.RenewContractRequestViewModels.SendRenewContractRequest
{
    public class SendRenewContractRequestRequest
    {

        [Required]
        public int StudentId { get; set; }
        [Required]
        public int Month { get; set; }

        public static ContractRenewalForm EntityFromRequest(SendRenewContractRequestRequest request, Contract contract)
        {
            return new ContractRenewalForm()
            {
                Month = request.Month,
                StudentId = request.StudentId,
                ContractId = contract.ContractId,
                CreatedDate = DateTime.Now.AddHours(GlobalParams.TimeZone),
                LastUpdated = DateTime.Now.AddHours(GlobalParams.TimeZone),
                Status = RequestStatus.Pending,
            };
        }
    }
}