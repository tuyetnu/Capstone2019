using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Services.MoneyServices;
using DormyWebService.ViewModels.PaymentModels;
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
        [HttpPost("SendWaterAndElectricNumber")]
        public async Task<ActionResult> SendWaterAndElectricNumber(SendNumberAndElectricNumberRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                await _monthlyBillService.SendWaterAndElectricNumber(request);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        //For Test
        [HttpGet("GenerateStudentBill")]
        public async Task<ActionResult> GenerateStudentBill()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                await _monthlyBillService.GenerateStudentBill();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize(Roles = Role.Staff)]
        [HttpGet("GetRoomAndPreviousNumber")]
        public async Task<ActionResult<List<RoomAndCurrentNumber>>> GetRoomAndPreviousNumber()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return await _monthlyBillService.GetRoomAndPreviousNumber();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        //[Authorize(Roles = Role.Staff)]
        //[HttpGet("GetStudentBill")]
        //public async Task<ActionResult<StudentBillResponse>> GetStudentBill(StudentBillRequest studentBillRequest)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }
        //        return _monthlyBillService.GetStudentBill(studentBillRequest);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }

        //}
    }
}