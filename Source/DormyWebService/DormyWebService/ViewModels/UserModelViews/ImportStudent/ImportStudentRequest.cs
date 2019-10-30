using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.RoomEntities;
using DormyWebService.Utilities;

namespace DormyWebService.ViewModels.UserModelViews.ImportStudent
{
    public class ImportStudentRequest
    {
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
        [RegularExpression(@"^(?:[0-9]{9}|[0-9]{12})$", ErrorMessage = "IdentityNumber can have 9 or 12 numeric characters")]
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

        [Required]
        [RegularExpression(@"^([0-2][0-9]|(3)[0-1])(\/)(((0)[0-9])|((1)[0-2]))(\/)\d{4}$", ErrorMessage = "dd/mm/yyyy")]
        public string BirthDay { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public static Student NewStudentFromRequest(ImportStudentRequest request, int evaluationPoint)
        {
            return new Student()
            {
                Name = request.Name,
                Address = request.Address,
                StartedSchoolYear = request.StartedSchoolYear,
                Term = request.Term,
                IdentityNumber = request.IdentityNumber,
                StudentCardNumber = request.StudentCardNumber,
                Gender = request.Gender,
                PhoneNumber = request.PhoneNumber,
                BirthDay = DateTime.ParseExact(request.BirthDay, GlobalParams.BirthDayFormat, CultureInfo.InvariantCulture),
                EvaluationScore = evaluationPoint,
                AccountBalance = 0,
                User = new User()
                {
                    Role = Role.Student,
                    Status = UserStatus.Active,
                    Email = request.Email
                },
                Email = request.Email,
        };
        }
    }
}