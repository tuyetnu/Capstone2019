using System;
using System.ComponentModel.DataAnnotations;
using DormyWebService.Entities.MoneyEntities;
using DormyWebService.Utilities;

namespace DormyWebService.ViewModels.MoneyViewModels.DepositMoney
{
    public class DepositMoneyRequest
    {
        [Required]
        public int StudentId { get; set; }
        [Required]
        public decimal Amount { get; set; }

        public static MoneyTransaction EntityFromRequest(decimal originalBalance, decimal resultBalance, DepositMoneyRequest request)
        {
            return new MoneyTransaction()
            {
                StudentId = request.StudentId,
                Date = DateTime.Now.AddHours(7),
                MoneyAmount = request.Amount,
                OriginalBalance = originalBalance,
                ResultBalance = resultBalance,
                Type = GlobalParams.ParamTransactionTypeDepositMoney
            };
        }
    }
}