using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.ParamEntities;

namespace DormyWebService.ViewModels.UserModelViews.GetStudentProfile
{
    public class GetStudentProfileResponse
    {
        public int Id { get; set; }
        public bool Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string BirthDay { get; set; }
        public string IdentityNumber { get; set; }
        public string IdentityCardImageIrl { get; set; }
        public int PriorityTypeId { get; set; }
        public GetStudentProfileRoom Room { get; set; }
        public bool IsRoomLeader { get; set; }
        public int EvaluationScore { get; set; }

        public static GetStudentProfileResponse MapFromStudent(Student student)
        {
            return new GetStudentProfileResponse()
            {
                Id = student.StudentId,
                //if null then room is null
                Room = (student.Room != null) ? new GetStudentProfileRoom()
                {
                    Id = student.Room.RoomId,
                    Name = student.Room.Name,

                }: null,
                PriorityTypeId = student.PriorityType,
                Gender = student.Gender,
                Address = student.Address,
                BirthDay = student.Address,
                EvaluationScore = student.EvaluationScore,
                IdentityCardImageIrl = student.IdentityCardImageUrl,
                IdentityNumber = student.IdentityNumber,
                IsRoomLeader = student.IsRoomLeader,
                PhoneNumber = student.PhoneNumber,
            };
        }
    }
}