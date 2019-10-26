namespace DormyWebService.Utilities
{
    public static class GlobalParams
    {
        //Param Types
        public const int ParamTypeNotificationType = 0;
        public const int ParamTypeTransactionType = 1;
        public const int ParamTypeStudentPriorityType = 2;
        public const int ParamTypeAcceptedEmailHost = 3;
        public const int ParamTypeRoomType = 4;
        public const int ParamTypeDepositMoneyParam = 5;
        public const int ParamTypeIssueType = 6;
        public const int ParamTransactionTypeDepositMoney = 13;

        //Depository params
        public const int ParamDepositMoneyUpperLimit = 14;
        public const int ParamDepositMoneyLowerLimit = 15;
        public const int ParamDepositMoneyStep = 16;

        public const decimal AcceptableDecimalMistake = (decimal) 0.0001;

        //Date Formats
        public const string DateTimeResponseFormat = "dd/MM/yyyy HH:mm:ss";
        public const string BirthDayFormat = "dd/MM/yyyy";
    }
}