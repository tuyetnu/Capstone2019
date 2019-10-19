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
using DormyWebService.Utilities;
using DormyWebService.ViewModels.UserModelViews;
using DormyWebService.ViewModels.UserModelViews.Login;
using Microsoft.AspNetCore.Authorization;

namespace DormyWebService.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Activate or disable user, for debug only, use this function in Student control instead
        /// </summary>
        /// <param name="id"></param>
        /// /// <param name="status">Target status</param>
        // PUT: api/Users/5
        [HttpPut("ChangeStatus/{id}")]
        public async Task<ActionResult<User>> ChangeStatus(int id, string status)
        {
            return await _userService.ChangeStatus(id, status);
        }

        /// <summary>
        /// For Login
        /// </summary>
        // POST: api/Users/Login[HttpPost]
        //Don't need access token to use this'
        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<LoginSuccessUser>> Login( [FromBody] SocialUser socialUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                return await _userService.Authenticate(socialUser.IdToken, socialUser.Email);
            }
            catch (HttpStatusCodeException e)
            {
                switch (e.StatusCode)
                {
                    case 400: return BadRequest("Request is invalid");
                    case 404: return NotFound("Could not find email in Google API");
                    default: return StatusCode(500, "Internal server error");
                }

            }
            
        }
    }
}
