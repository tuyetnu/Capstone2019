using DormyWebService.Entities.ContractEntities;
using DormyWebService.Entities.TicketEntities;
using DormyWebService.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DormyWebService.ViewModels.TicketViewModels.CancelContract.SendCancelContractRequest
{
    public class SendCancelContractFormRequest
    {
        [Required]
        public int StudentId { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(500)]
        public string Reason { get; set; }

        public static CancelContractForm EntityFromRequest(SendCancelContractFormRequest request, Contract contract)
        {
            return new CancelContractForm()
            {
                //Month = request.Month,
                StudentId = request.StudentId,
                //ContractId = contract.ContractId,
                CreatedDate = DateTime.Now.AddHours(GlobalParams.TimeZone),
                LastUpdated = DateTime.Now.AddHours(GlobalParams.TimeZone),
                Status = RequestStatus.Pending,

            };
        }
    }
}
