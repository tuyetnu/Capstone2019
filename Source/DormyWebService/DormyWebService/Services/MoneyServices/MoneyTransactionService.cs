using System.Net;
using System.Threading.Tasks;
using DormyWebService.Repositories;
using DormyWebService.Services.ParamServices;
using DormyWebService.Services.UserServices;
using DormyWebService.Utilities;
using DormyWebService.ViewModels.MoneyViewModels.DepositMoney;

namespace DormyWebService.Services.MoneyServices
{
    public class MoneyTransactionService : IMoneyTransactionService
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly IStudentService _studentService;
        private readonly IParamService _paramService;

        public MoneyTransactionService(IRepositoryWrapper repoWrapper, IStudentService studentService, IParamService paramService)
        {
            _repoWrapper = repoWrapper;
            _studentService = studentService;
            _paramService = paramService;
        }

        public async Task<DepositMoneyResponse> DepositMoney(DepositMoneyRequest request)
        {
            //Check Amount
            var upperLimit = (await _paramService.FindById(GlobalParams.ParamDepositMoneyUpperLimit)).Value;
            var lowerLimit = (await _paramService.FindById(GlobalParams.ParamDepositMoneyLowerLimit)).Value;
            var step = (await _paramService.FindById(GlobalParams.ParamDepositMoneyStep)).Value;
            if (request.Amount > upperLimit || request.Amount < lowerLimit || (request.Amount / step) > GlobalParams.AcceptableDecimalMistake)
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, "amount must be between " + lowerLimit + " and " + upperLimit + " and must be multiple of " + step);
            }

            //Find student in database
            var student = await _studentService.FindById(request.StudentId);

            var originalBalance = student.AccountBalance;

            //Change student's balance
            student.AccountBalance += request.Amount;

            var resultBalance = student.AccountBalance;

            //Update information in database
            student = await _repoWrapper.Student.UpdateAsync(student, student.StudentId);

            //Create MoneyTransaction entity
            var moneyTransaction = DepositMoneyRequest.EntityFromRequest(originalBalance, resultBalance, request);

            //Create MoneyTransaction in database
            moneyTransaction = await _repoWrapper.MoneyTransaction.CreateAsync(moneyTransaction);

            //Return View Model
            return DepositMoneyResponse.ResponseFromModel(moneyTransaction);
        }
    }
}