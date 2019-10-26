using System.Threading.Tasks;
using DormyWebService.ViewModels.MoneyViewModels.DepositMoney;

namespace DormyWebService.Services.MoneyServices
{
    public interface IMoneyTransactionService
    {
        Task<DepositMoneyResponse> DepositMoney(DepositMoneyRequest request);
    }
}