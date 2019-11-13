using DormyWebService.Entities.MoneyEntities;
using DormyWebService.Entities.RoomEntities;
using DormyWebService.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DormyWebService.ViewModels.RoomMonthlyBillViewModel.SendWaterAndElectric
{
    public class SendNumberAndElectricNumberRequest
    {
        [Required]
        public int RoomId { get; set; }
        [Required]
        public int WaterNumber { get; set; }
        [Required]
        public int ElectricNumber { get; set; }

    }
}
