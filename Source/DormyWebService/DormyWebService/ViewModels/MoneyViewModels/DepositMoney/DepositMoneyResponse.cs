using System;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.MoneyEntities;
using DormyWebService.Entities.RoomEntities;
using DormyWebService.Utilities;

namespace DormyWebService.ViewModels.MoneyViewModels.DepositMoney
{
    public class DepositMoneyResponse
    {
        public int MoneyTransactionId { get; set; }
        public int StudentId { get; set; }
        public int Type { get; set; }
        public decimal OriginalBalance { get; set; }
        public decimal MoneyAmount { get; set; }
        public decimal ResultBalance { get; set; }
        public string Date { get; set; }

        public static DepositMoneyResponse ResponseFromModel(MoneyTransaction transaction)
        {
            return new DepositMoneyResponse()
            {
                StudentId = transaction.StudentId,
                Type = transaction.Type,
                Date = transaction.Date.ToString(GlobalParams.DateTimeResponseFormat),
                OriginalBalance = transaction.OriginalBalance,
                ResultBalance = transaction.ResultBalance,
                MoneyAmount = transaction.MoneyAmount,
                MoneyTransactionId = transaction.MoneyTransactionId
            };
        }
    }
}