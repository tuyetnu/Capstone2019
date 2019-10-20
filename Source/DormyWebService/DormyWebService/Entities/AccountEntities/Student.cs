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
        public int StartedSchoolYear { get; set; }

        //CMND
        [Required]
        [MinLength(9)]
        [MaxLength(12)]
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
        public string PhoneNumber { get; set; }

        [Required]
        public bool IsRoomLeader { get; set; }

        //Điểm đánh giá
        public int EvaluationScore { get; set; }

        [Required]
        [Column(TypeName = "Money")]
        public decimal AccountBalance { get; set; }

        public virtual ICollection<Contract> Contracts { get; set; }
    }
}