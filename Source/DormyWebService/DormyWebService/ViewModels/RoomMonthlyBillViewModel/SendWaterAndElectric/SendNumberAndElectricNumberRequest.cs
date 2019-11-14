using System.ComponentModel.DataAnnotations;

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
