using DormyWebService.Entities.AccountEntities;
using DormyWebService.Utilities;

namespace DormyWebService.ViewModels.UserModelViews.ImportStudent
{
    public class ImportStudentResponse
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public int StartedSchoolYear { get; set; }
        public string IdentityNumber { get; set; }
        public string StudentCardNumber { get; set; }
        public string BirthDay { get; set; }
        public int Term { get; set; }
        public bool Gender { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }

        public static ImportStudentResponse CreateFromStudent(Student student)
        {
            return new ImportStudentResponse()
            {
                Name = student.Name,
                StudentId = student.StudentId,
                BirthDay = student.BirthDay.ToString(GlobalParams.BirthDayFormat),
                StudentCardNumber = student.StudentCardNumber,
                Term = student.Term,
                StartedSchoolYear = student.StartedSchoolYear,
                Address = student.Address,
                Gender = student.Gender,
                IdentityNumber = student.IdentityNumber,
                PhoneNumber = student.PhoneNumber,
                Status = student.User.Status,
                Email = student.User.Email,
            };
        }
    }
}