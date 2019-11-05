namespace DormyWebService.Utilities
{
    public static class GlobalParams
    {
        //Gender
        public const bool Male = true;
        public const bool Female = false;

        //Param Types
        public const int ParamTypeNotificationType = 0;
        public const int ParamTypeTransactionType = 1;
        public const int ParamTypeStudentPriorityType = 2;
        public const int ParamTypeAcceptedEmailHost = 3;
        public const int ParamTypeRoomType = 4;
        public const int ParamTypeDepositMoneyParam = 5;
        public const int ParamTypeIssueType = 6;
        public const int ParamTypeEquipmentType = 7;
        public const int ParamTypeEvaluationParam = 8;
        public const int ParamTypeContractParam = 9;
        public const int ParamTypeRoomBookingParam = 10;
        public const int ParamTypeRoomTransferParam = 11;

        //Param
        public const int ParamNoPriorityType = 3;
        public const int ParamStandardRoomType = 11;
        public const int ParamServiceRoomType = 12;
        //DepositMoney Param
        public const int ParamTransactionTypeDepositMoney = 13;
        //Depository params
        public const int ParamDepositMoneyUpperLimit = 14;
        public const int ParamDepositMoneyLowerLimit = 15;
        public const int ParamDepositMoneyStep = 16;
        public const int ParamDefaultEvaluationPoint = 19;
        public const int ParamContractRenewalEvaluationPointMargin = 20;
        public const int ParamMaxYearForStaying = 24;
        public const int MaxDayForApproveRoomBooking = 25;
        public const int MaxDayForCompleteRoomBooking = 26;
        public const int MaxDayForApproveRoomTransfer = 27;
        public const int MaxDayForCompleteRoomTransfer = 28;

        //Acceptable Decimal Mistake use for calculating money
        public const decimal AcceptableDecimalMistake = (decimal) 0.0001;

        //Date Formats
        public const int TimeZone = 7;
        public const string DateTimeResponseFormat = "dd/MM/yyyy HH:mm:ss";
        public const string BirthDayFormat = "dd/MM/yyyy";

        //Default Evaluation Point
        public const int DefaultEvaluationPoint = 100;
        public const int DefaultContractRenewalEvaluationPointMargin = 50;

        public const string DefaultAutoRejectRoomBookingReason = "Time expired";
        public const string DefaultAutoRejectRoomTransferReason = "Time expired";
    }
}