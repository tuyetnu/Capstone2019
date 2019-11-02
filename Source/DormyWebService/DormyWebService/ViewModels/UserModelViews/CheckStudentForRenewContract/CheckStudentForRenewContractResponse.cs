﻿namespace DormyWebService.ViewModels.UserModelViews.CheckStudentForRenewContract
{
    public class CheckStudentForRenewContractResponse
    {
        public bool HasInValidTrainingPoint { get; set; }
        public bool HasStayedMoreThanPermittedYear { get; set; }
        public bool ContractIsActiveNextMonth { get; set; }
        public int NumberOfRoomTransferRequest { get; set; } 
    }
}