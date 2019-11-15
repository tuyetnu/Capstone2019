using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DormyWebService.Entities.MoneyEntities;

namespace DormyWebService.ViewModels.PaymentModels
{
    public class RoomAndCurrentNumber
    {
        public RoomAndCurrentNumber()
        {
        }

        public RoomAndCurrentNumber(RoomMonthlyBill b)
        {
            this.RoomId = b.RoomId;
            this.RoomName = b.Room.Name;
            this.CurrentElectricityNumber = b.NewElectricityNumber;
            this.CurrentWaterNumber = b.NewWaterNumber;
            this.HasNewNumber = false;
        }

        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public int CurrentWaterNumber { get; set; }
        public int CurrentElectricityNumber { get; set; }
        public bool HasNewNumber { get; set; }
    }
}
