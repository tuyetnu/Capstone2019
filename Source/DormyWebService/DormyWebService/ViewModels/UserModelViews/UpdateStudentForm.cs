using System.ComponentModel.DataAnnotations;

namespace DormyWebService.ViewModels.UserModelViews
{
    public class UpdateStudentForm
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }

        //Năm nhập học
        [Required]
        public int StartedSchoolYear { get; set; }

        //CMND
        [Required]
        public string IdentityNumber { get; set; }

        //MSSV
        [Required]
        public string StudentCardNumber { get; set; }

        [Required]
        //Khóa học
        public int Term { get; set; }

        [Required]
        //Viện ưu tiên
        //Param
        public int PriorityType { get; set; }

        [Required]
        public bool Gender { get; set; }

        //Địa chỉ
        [MinLength(3)]
        [MaxLength(100)]
        public string Address { get; set; }
    }
}