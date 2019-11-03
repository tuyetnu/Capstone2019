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

        //Param
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
        public const int ParamMaxDayForRoomBooking = 25;

        

        

        //Acceptable Decimal Mistake use for calculating money
        public const decimal AcceptableDecimalMistake = (decimal) 0.0001;

        //Date Formats
        public const int TimeZone = 7;
        public const string DateTimeResponseFormat = "dd/MM/yyyy HH:mm:ss";
        public const string BirthDayFormat = "dd/MM/yyyy";

        //GUID Format, used for generating equipmentId
        public const string GuidFormat = "N";

        //Default Evaluation Point
        public const int DefaultEvaluationPoint = 100;
        public const int DefaultContractRenewalEvaluationPointMargin = 50;
    }
}