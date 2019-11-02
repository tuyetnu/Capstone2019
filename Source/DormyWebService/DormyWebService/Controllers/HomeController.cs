using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DormyWebService.Services.NewFolder;
using DormyWebService.Utilities;
using DormyWebService.ViewModels.HomeModelView;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DormyWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        /// <summary>
        /// Get values of a student for home screen of the mobile app, for authorized users
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<HomeResponse>> GetInitialValues(int id)
        {
            return await _homeService.GetInitialValues(id);
        }

        /// <summary>
        /// Get current date in system
        /// </summary>
        /// <returns></returns>
        [HttpGet("Date")]
        public ActionResult<string> GetCurrentDate()
        {
            return DateTime.Now.AddHours(GlobalParams.TimeZone).ToString(GlobalParams.BirthDayFormat);
        }

    }
}