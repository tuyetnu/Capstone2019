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
        public async Task<ActionResult<List<GetUserResponse>>> AdvancedGetUser(string sorts, string filters, int? page, int? pageSize)
        {
            try
            {
                return await _userService.AdvancedGetUser(sorts, filters, page, pageSize);
            }
            catch (HttpStatusCodeException e)
            {
                return StatusCode(e.StatusCode, e.Message);
            }
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

            try
            {
                return await _userService.ChangeUserRole(userId, role);
            }
            catch (HttpStatusCodeException e)
            {
                return StatusCode(e.StatusCode, e.Message);
            }
        }
    }
}
