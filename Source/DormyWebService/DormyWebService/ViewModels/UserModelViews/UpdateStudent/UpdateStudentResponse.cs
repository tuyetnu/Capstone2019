namespace DormyWebService.ViewModels.UserModelViews.UpdateStudent
{
    public class UpdateStudentResponse
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public int StartedSchoolYear { get; set; }
        public string IdentityNumber { get; set; }
        public string StudentCardNumber { get; set; }
        public int Term { get; set; }
        public bool Gender { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}