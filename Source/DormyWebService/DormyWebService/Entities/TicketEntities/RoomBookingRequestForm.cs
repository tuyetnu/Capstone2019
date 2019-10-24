using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.RoomEntities;

namespace DormyWebService.Entities.TicketEntities
{
    public class RoomBookingRequestForm
    {
        //TODO: Nhớ làm đc đăng ký trong khoảng tg, ngày nào trong tuần, và khi nào thì request hết hạn.
        //TODO: Nhớ làm bảng cho tùy chỉnh các khoản TG là khi nào.

        [Key]
        public int RoomBookingRequestFormId { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime LastUpdated { get; set; }

        [Required]
        public int Month { get; set; }

        //Current Status
        [Required]
        public string Status { get; set; }

        [Required]
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        
        public Student Student { get; set; }

        [ForeignKey("Staff")]
        public int? StaffId { get; set; }

        public virtual Staff Staff { get; set; }

        [Required]
        public int TargetRoomType { get; set; }

        public string Reason { get; set; }
    }
}