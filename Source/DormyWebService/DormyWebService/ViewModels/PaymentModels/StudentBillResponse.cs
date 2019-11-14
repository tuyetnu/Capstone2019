using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DormyWebService.Entities.MoneyEntities;

namespace DormyWebService.ViewModels.PaymentModels
{
    public class StudentBillResponse
    {
        public StudentBillResponse(StudentMonthlyBill find)
        {
            this.Total = (int) find.Total;
            this.WaterNumber = find.RoomMonthlyBill.NewWaterNumber - find.RoomMonthlyBill.PreviousWaterNumber;
            this.ElectricityNumber = find.RoomMonthlyBill.NewElectricityNumber - find.RoomMonthlyBill.PreviousElectricityNumber;
        }

        public int Total { get; set; }
        public int WaterNumber { get; set; }
        public int ElectricityNumber { get; set; }
        public int PricePerWarter { get; set; }
        public int PricePerElectricity { get; set; }
        public int PricePerRoom { get; set; }
        public int WaterBill { get; set; }
        public int ElectricityBill { get; set; }
        public Decimal RoomBill { get; set; }
        public bool IsPaid { get; set; }
    }
}
