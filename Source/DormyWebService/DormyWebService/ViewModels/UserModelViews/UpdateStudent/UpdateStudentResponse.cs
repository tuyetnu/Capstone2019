using System;
using DormyWebService.Entities.AccountEntities;

namespace DormyWebService.ViewModels.UserModelViews.UpdateStudent
{
    public class UpdateStudentResponse
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

        public static UpdateStudentResponse CreateFromStudent(Student student)
        {
            return new UpdateStudentResponse()
            {
                Name = student.Name,
                StudentId = student.StudentId,
                BirthDay = student.BirthDay.ToString("dd/MM/yyyy HH:mm:ss"),
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