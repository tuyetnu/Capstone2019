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
                StudentId = request.StudentId,
                CancelationDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1),
                CreatedDate = DateTime.Now.AddHours(GlobalParams.TimeZone),
                LastUpdated = DateTime.Now.AddHours(GlobalParams.TimeZone),
                Reason = request.Reason,
                Status = RequestStatus.Pending,
            };
        }
    }
}
