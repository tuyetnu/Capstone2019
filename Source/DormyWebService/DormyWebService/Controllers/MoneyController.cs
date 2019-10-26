using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Services.MoneyServices;
using DormyWebService.ViewModels.MoneyViewModels.DepositMoney;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DormyWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoneyController : ControllerBase
    {
        private IMoneyTransactionService _moneyTransactionService;

        public MoneyController(IMoneyTransactionService moneyTransactionService)
        {
            _moneyTransactionService = moneyTransactionService;
        }

        /// <summary>
        /// Deposit money into student account, for student
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.Student)]
        [HttpPost]
        public async Task<ActionResult<DepositMoneyResponse>> DepositMoney(DepositMoneyRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return await _moneyTransactionService.DepositMoney(request);
        }
    }
}