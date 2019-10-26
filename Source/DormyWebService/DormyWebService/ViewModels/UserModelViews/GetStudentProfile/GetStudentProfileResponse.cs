using System;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.RoomEntities;
using DormyWebService.Utilities;

namespace DormyWebService.ViewModels.UserModelViews.GetStudentProfile
{
    public class GetStudentProfileResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int StartedSchoolYear { get; set; }
        public int Term { get; set; }
        public string StudentCardNumber { get; set; }
        public bool Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string BirthDay { get; set; }
        public string IdentityNumber { get; set; }
        public string IdentityCardImageIrl { get; set; }
        public GetStudentProfileResponsePriorityType PriorityType { get; set; }
        public GetStudentProfileResponseRoom Room { get; set; }
        public bool IsRoomLeader { get; set; }
        public int EvaluationScore { get; set; }

        public static GetStudentProfileResponse MapFromStudent(Student student, Entities.ParamEntities.Param param, User user, Room room)
        {
            GetStudentProfileResponsePriorityType priorityType = null;

            if (param != null)
            {
                priorityType = new GetStudentProfileResponsePriorityType()
                {
                    Name = param.Name,
                    ParamTypeId = param.ParamTypeId,
                    ParamId = param.ParamId
                };
            }

            return new GetStudentProfileResponse()
            {
                Id = student.StudentId,
                Email = user.Email,
                //if null then room is null
                Room = (room != null) ? new GetStudentProfileResponseRoom()
                {
                    Id = room.RoomId,
                    Name = room.Name,

                }: null,
                PriorityType = priorityType,
                Gender = student.Gender,
                Address = student.Address,
                BirthDay = student.BirthDay.ToString(GlobalParams.BirthDayFormat),
                EvaluationScore = student.EvaluationScore,
                IdentityCardImageIrl = student.IdentityCardImageUrl,
                IdentityNumber = student.IdentityNumber,
                IsRoomLeader = student.IsRoomLeader,
                PhoneNumber = student.PhoneNumber,
                Name = student.Name,
                StartedSchoolYear = student.StartedSchoolYear,
                StudentCardNumber = student.StudentCardNumber,
                Term = student.Term,
            };
        }
    }
}