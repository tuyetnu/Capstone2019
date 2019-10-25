using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormyWebService.Entities.ContractEntities;
using DormyWebService.Entities.RoomEntities;
using DormyWebService.Entities.TicketEntities;
using Sieve.Attributes;

namespace DormyWebService.Entities.AccountEntities
{
    [Table("Students")]
    public class Student
    {
        //One-to-One relationship with User
        [Key]
        [ForeignKey("User")]
        [Sieve(CanFilter = true, CanSort = true)]
        public int StudentId { get; set; }

        //One-to-one user
        public virtual User User { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        [Sieve(CanFilter = true, CanSort = true)]
        public string Name { get; set; }

        //Năm nhập học
        [Required]
        [RegularExpression(@"^[0-9]*$")]
        [Sieve(CanFilter = true, CanSort = true)]
        public int StartedSchoolYear { get; set; }

        //CMND
        [Required]
        [MaxLength(12)]
        [RegularExpression(@"^(?:[0-9]{9}|[0-9]{12})$")]
        [Sieve(CanFilter = true, CanSort = true)]
        public string IdentityNumber { get; set; }

        //Hình CMND
        public string IdentityCardImageUrl { get; set; }

        //MSSV
        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public string StudentCardNumber { get; set; }

        //Hình thẻ SV
        public string StudentCardImageUrl { get; set; }

        //Khóa học
        [Sieve(CanFilter = true, CanSort = true)]
        public int Term { get; set; }

        //Viện ưu tiên
        //Param
        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public int PriorityType { get; set; }

        //Hình đối tượng ưu tiên
        public string PriorityImageUrl { get; set; }

        [ForeignKey("Room")]
        public int? RoomId { get; set; }

        //Phòng đang ở
        public Room Room { get; set; }

        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public bool Gender { get; set; }

        //Địa chỉ
        [MinLength(3)]
        [MaxLength(100)]
        [Sieve(CanFilter = true, CanSort = true)]
        public string Address { get; set; }

        //SĐT
        //Check if number only
        [RegularExpression(@"^[0-9]*$")]
        [Sieve(CanFilter = true, CanSort = true)]
        public string PhoneNumber { get; set; }

        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public bool IsRoomLeader { get; set; }

        //Điểm đánh giá
        [Sieve(CanFilter = true, CanSort = true)]
        public int EvaluationScore { get; set; }

        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public decimal AccountBalance { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public DateTime BirthDay { get; set; }
    }
}