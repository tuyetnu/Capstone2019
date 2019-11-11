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

        public static RoomMonthlyBill EntityFormRequest(Room room, RoomMonthlyBill previousBill, PricePerUnit pricePerUnit,int waterNumber, int electricNumber)
        {
            decimal waterBill = (waterNumber - previousBill.NewWaterNumber) * pricePerUnit.WaterPricePerUnit;
            decimal electricBill = (electricNumber - previousBill.NewElectricityNumber) * pricePerUnit.ElectricityPricePerUnit;
            return new RoomMonthlyBill()
            {
                Room = room,
                PreviousWaterNumber = previousBill.NewWaterNumber,
                NewWaterNumber = waterNumber,
                WaterBill = waterBill,
                PreviousElectricityNumber = previousBill.NewWaterNumber,
                NewElectricityNumber = electricNumber,
                ElectricityBill = electricBill,
                TargetMonth = DateTime.Now.Month,
                TargetYear = DateTime.Now.Year,
                CreatedDate = DateTime.Now.AddHours(GlobalParams.TimeZone),
                LastUpdated = DateTime.Now.AddHours(GlobalParams.TimeZone),
                IsPaid = false,
                //TODO


            };
        }

    }
}
