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
using DormyWebService.ViewModels.Debug.ChangeUserRole;
using DormyWebService.ViewModels.UserModelViews;
using DormyWebService.ViewModels.UserModelViews.GetUser;
using DormyWebService.ViewModels.UserModelViews.Login;
using Microsoft.AspNetCore.Authorization;
using DormyWebService.ViewModels.UserModelViews.CheckToken;
using DormyWebService.ViewModels.UserModelViews.SendFCMDeviceToken;

namespace DormyWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Get user with condition, for admin and staff
        /// </summary>
        /// <param name="sorts">See GET /api/Rooms for examples</param>
        /// <param name="filters">See GET /api/Rooms for examples</param>
        /// <param name="page">See GET /api/Rooms for examples</param>
        /// <param name="pageSize">See GET /api/Rooms for examples</param>
        /// <returns></returns>
        [Authorize(Roles = Role.Admin + "," + Role.Staff)]
        [HttpGet("AdvancedGet")]
        public async Task<ActionResult<List<GetUserResponse>>> AdvancedGetUser(string sorts, string filters, int? page,
            int? pageSize)
        {
            return await _userService.AdvancedGetUser(sorts, filters, page, pageSize);
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
        public async Task<ActionResult<LoginSuccessUser>> Login([FromBody] SocialUser socialUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return await _userService.Authenticate(socialUser.IdToken, socialUser.Email);
        }

        /// <summary>
        /// Find all user for debug
        /// </summary>
        /// <returns></returns>
        [HttpGet("Debug/GetAllUser")]
        public async Task<ActionResult<List<User>>> FindAll()
        {
            return await _userService.DebugFindAll();
        }

        /// <summary>
        /// Change user role for debug
        /// </summary>
        [HttpPut("Debug/ChangeUserRole")]
        public async Task<ActionResult<DebugChangeUserRoleResponse>> ChangeUserRole(int userId, string role)
        {
            if (!Role.IsRole(role))
            {
                return BadRequest("Role is not valid,");
            }

            return await _userService.ChangeUserRole(userId, role);
        }
        /// <summary>
        /// check token for keep logged in, for Authorized user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("CheckToken/{userId}")]
        public async Task<ActionResult<CheckTokenResponse>> checkToken(int userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return await _userService.CheckTokenAsync(userId);
        }

        [Authorize]
        [HttpGet("Logout/{userId}")]
        public async Task<ActionResult<string>> Logout(int userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return await _userService.Logout(userId);
        }

        [Authorize]
        [HttpPost]
        [Route("SendFMCDeviceTokenToServer")]
        public async Task<ActionResult<string>> SendFCMDeviceTokenToServer(SendFCMDeviceTokenRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return await _userService.SendFCMDeviceTokenToServer(request);
        }

    }
}
