using System;
using System.ComponentModel.DataAnnotations;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.RoomEntities;

namespace DormyWebService.Entities.TicketEntities
{
    public class DormitoryReservationForm
    {
        //TODO: Nhớ làm đc đăng ký trong khoảng tg, ngày nào trong tuần, và khi nào thì request hết hạn.
        //TODO: Nhớ làm bảng cho tùy chỉnh các khoản TG là khi nào.

        [Key]
        public int DormitoryReservationFormId { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime LastUpdated { get; set; }

        [Required]
        private int Month { get; set; }

        //Param
        //Current Status
        [Required]
        public int Status { get; set; }

        [Required]
        public Student Student { get; set; }

        public Staff Staff { get; set; }

        [Required]
        public RoomType TargetRoomType { get; set; }
    }
}