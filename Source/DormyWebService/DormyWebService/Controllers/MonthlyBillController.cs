using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Services.MoneyServices;
using DormyWebService.ViewModels.RoomMonthlyBillViewModel.SendWaterAndElectric;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DormyWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonthlyBillController : ControllerBase
    {
        private readonly IRoomMonthlyBillService _monthlyBillService;

        public MonthlyBillController(IRoomMonthlyBillService monthlyBillService)
        {
            _monthlyBillService = monthlyBillService;
        }

        [Authorize(Roles = Role.Staff)]
        [HttpPost]
        public async Task<ActionResult<SendNumberAndElectricNumberResponse>> SendWaterAndElectricNumber(SendNumberAndElectricNumberRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return await _monthlyBillService.SendWaterAndElectricNumber(request);
        }
    }
}