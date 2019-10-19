namespace DormyWebService.ViewModels.UserModelViews.GetAllStudent
{
    public class GetAllStudentResponse
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public int StartedSchoolYear { get; set; }
        public string IdentityNumber { get; set; }
        public string StudentCardNumber { get; set; }
        public int Term { get; set; }
        public int PriorityType { get; set; }
        public bool Gender { get; set; }
        public string Address { get; set; }
    }
}