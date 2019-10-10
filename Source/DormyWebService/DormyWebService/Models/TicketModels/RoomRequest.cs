using System;
using System.ComponentModel.DataAnnotations;
using DormyWebService.Models.AccountModels;
using DormyWebService.Models.Enums;
using DormyWebService.Models.RoomModels;

namespace DormyAppService.Models.TicketModels
{
    public class RoomRequest
    {
        //TODO: Nhớ làm đc đăng ký trong khoảng tg, ngày nào trong tuần, và khi nào thì request hết hạn.
        //TODO: Nhớ làm bảng cho tùy chỉnh các khoản TG là khi nào.

        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime LastUpdated { get; set; }

        [Required]
        private int Month { get; set; }

        //Current Status
        [Required]
        public RequestStatusEnum Status { get; set; }

        [Required]
        public Student Student { get; set; }

        public Staff Staff { get; set; }

        [Required]
        public RoomType TargetRoomType { get; set; }
    }
}