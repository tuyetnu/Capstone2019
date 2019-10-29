using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.RoomEntities;
using Sieve.Attributes;

namespace DormyWebService.Entities.TicketEntities
{
    public class RoomBookingRequestForm
    {
        //TODO: Nhớ làm đc đăng ký trong khoảng tg, ngày nào trong tuần, và khi nào thì request hết hạn.
        //TODO: Nhớ làm bảng cho tùy chỉnh các khoản TG là khi nào.

        [Key]
        [Sieve(CanFilter = true, CanSort = true)]
        public int RoomBookingRequestFormId { get; set; }

        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public DateTime CreatedDate { get; set; }

        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public DateTime LastUpdated { get; set; }

        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public int Month { get; set; }

        //Current Status
        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public string Status { get; set; }

        [Required]
        [ForeignKey("Student")]
        [Sieve(CanFilter = true, CanSort = true)]
        public int StudentId { get; set; }
        
        public Student Student { get; set; }

        [ForeignKey("Staff")]
        [Sieve(CanFilter = true, CanSort = true)]
        public int? StaffId { get; set; }

        public virtual Staff Staff { get; set; }

        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public int TargetRoomType { get; set; }

        public string Reason { get; set; }

        [Required]
        public string IdentityCardImageUrl { get; set; }

        [Required]
        public string StudentCardImageUrl { get; set; }

        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public int PriorityType { get; set; }

        [Required]
        public string PriorityImageUrl { get; set; }

        [ForeignKey("Room")]
        public int? RoomId { get; set; }

        public Room Room { get; set; }
    }
}