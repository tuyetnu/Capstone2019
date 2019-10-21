using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormyWebService.Entities.ContractEntities;
using DormyWebService.Entities.RoomEntities;

namespace DormyWebService.Entities.AccountEntities
{
    [Table("Students")]
    public class Student
    {
        //One-to-One relationship with User
        [Key]
        [ForeignKey("User")]
        public int StudentId { get; set; }

        //One-to-one user
        public virtual User User { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }

        //Năm nhập học
        [Required]
        [RegularExpression(@"^[0-9]*$")]
        public int StartedSchoolYear { get; set; }

        //CMND
        [Required]
        [MaxLength(12)]
        [RegularExpression(@"^(?:[0-9]{9}|[0-9]{12})$")]
        public string IdentityNumber { get; set; }

        //Hình CMND
        public string IdentityCardImageUrl { get; set; }

        //MSSV
        [Required]
        public string StudentCardNumber { get; set; }

        //Hình thẻ SV
        public string StudentCardImageUrl { get; set; }

        //Khóa học
        public int Term { get; set; }

        [Required]
        //Viện ưu tiên
        //Param
        public int PriorityType { get; set; }

        //Hình đối tượng ưu tiên
        public string PriorityImageUrl { get; set; }

        //Phòng đang ở
        public Room Room { get; set; }

        [Required]
        public bool Gender { get; set; }

        //Địa chỉ
        [MinLength(3)]
        [MaxLength(100)]
        public string Address { get; set; }

        //SĐT
        //Check if number only
        [RegularExpression(@"^[0-9]*$")]
        public string PhoneNumber { get; set; }

        [Required]
        public bool IsRoomLeader { get; set; }

        //Điểm đánh giá
        public int EvaluationScore { get; set; }

        [Required]
        [Column(TypeName = "Money")]
        public decimal AccountBalance { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDay { get; set; }

        public virtual ICollection<Contract> Contracts { get; set; }
    }
}