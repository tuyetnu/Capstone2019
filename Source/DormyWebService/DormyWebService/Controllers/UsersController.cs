using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DormyWebService.Entities;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Services;
using DormyWebService.Services.UserServices;
using DormyWebService.ViewModels.AccountModelViews;
using Microsoft.AspNetCore.Authorization;

namespace DormyWebService.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // PUT: api/Users/5
        [HttpPut("ChangeStatus/{id}")]
        public async Task<ActionResult<User>> ChangeStatus(int id, string status)
        {
            return await _userService.ChangeStatus(id, status);
        }

        // POST: api/Users/Login[HttpPost]
        //Don't need access token to use this'
        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<LoginSuccessUser>> Login(SocialUser socialUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return await _userService.Authenticate(socialUser);
        }
    }
}
