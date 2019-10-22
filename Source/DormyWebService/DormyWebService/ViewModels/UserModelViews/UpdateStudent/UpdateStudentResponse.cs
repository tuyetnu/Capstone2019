using System;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Utilities;

namespace DormyWebService.ViewModels.UserModelViews.UpdateStudent
{
    public class UpdateStudentResponse
    {
        public string Name { get; set; }
        public int StartedSchoolYear { get; set; }
        public string IdentityNumber { get; set; }
        public string StudentCardNumber { get; set; }
        public string BirthDay { get; set; }
        public int Term { get; set; }
        public bool Gender { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public static UpdateStudentResponse CreateFromStudent(Student student)
        {
            return new UpdateStudentResponse()
            {
                Name = student.Name,
                BirthDay = student.BirthDay.ToString(GlobalParams.BirthDayFormat),
                StudentCardNumber = student.StudentCardNumber,
                Term = student.Term,
                StartedSchoolYear = student.StartedSchoolYear,
                Address = student.Address,
                Gender = student.Gender,
                IdentityNumber = student.IdentityNumber,
                PhoneNumber = student.PhoneNumber
            };
        }
    }
}