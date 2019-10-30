namespace DormyWebService.ViewModels.UserModelViews.CheckStudentForRenewContract
{
    public class CheckStudentForRenewContractResponse
    {
        public bool HasInValidTrainingPoint { get; set; }
        public bool HasStayedMoreThanPermittedYear { get; set; }
    }
}