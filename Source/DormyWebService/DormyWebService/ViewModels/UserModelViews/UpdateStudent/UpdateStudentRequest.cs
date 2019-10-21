using System.ComponentModel.DataAnnotations;
using DormyWebService.Entities.AccountEntities;

namespace DormyWebService.ViewModels.UserModelViews.UpdateStudent
{
    public class UpdateStudentRequest
    {
        [Required]
        public int StudentId { get; set; }

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

        //MSSV
        [Required]
        public string StudentCardNumber { get; set; }

        [Required]
        //Khóa học
        public int Term { get; set; }

        [Required]
        public bool Gender { get; set; }

        //Địa chỉ
        [MinLength(3)]
        [MaxLength(100)]
        public string Address { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*$")]
        public string PhoneNumber { get; set; }

        public Student MapToStudent(Student student)
        {
            student.Name = Name;
            student.Address = Address;
            student.StartedSchoolYear = StartedSchoolYear;
            student.Term = Term;
            student.IdentityNumber = IdentityNumber;
            student.StudentCardNumber = StudentCardNumber;
            student.Gender = Gender;
            student.PhoneNumber = PhoneNumber;

            return student;
        }
    }
}