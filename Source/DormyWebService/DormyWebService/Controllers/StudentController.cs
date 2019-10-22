using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Services.UserServices;
using DormyWebService.Utilities;
using DormyWebService.ViewModels.UserModelViews;
using DormyWebService.ViewModels.UserModelViews.ChangeStudentStatus;
using DormyWebService.ViewModels.UserModelViews.GetAllStudent;
using DormyWebService.ViewModels.UserModelViews.GetStudentProfile;
using DormyWebService.ViewModels.UserModelViews.ImportStudent;
using DormyWebService.ViewModels.UserModelViews.UpdateStudent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DormyWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        //TODO: Determine Authorization
        [HttpGet]
        public async Task<ActionResult<List<GetAllStudentResponse>>> GetAllStudents()
        {
            try
            {
                return await _studentService.GetAllStudent();
            }
            catch (HttpStatusCodeException e)
            {
                return StatusCode(e.StatusCode, e.Message);
            }
        }

        /// <summary>
        /// Get student with condition, for admin and staff
        /// </summary>
        /// <param name="sorts">See GET /api/Rooms for examples</param>
        /// <param name="filters">See GET /api/Rooms for examples</param>
        /// <param name="page">See GET /api/Rooms for examples</param>
        /// <param name="pageSize">See GET /api/Rooms for examples</param>
        /// <returns></returns>
        [Authorize(Roles = Role.Admin + "," + Role.Staff)]
        [HttpGet("AdvancedGet")]
        public async Task<ActionResult<List<GetAllStudentResponse>>> AdvancedGetStudent(string sorts, string filters, int? page, int? pageSize)
        {
            try
            {
                return await _studentService.AdvancedGetStudent(sorts,filters,page,pageSize);
            }
            catch (HttpStatusCodeException e)
            {
                return StatusCode(e.StatusCode, e.Message);
            }
        }

        //        [HttpGet("{id}")]
        //        public async Task<ActionResult<FindByIdStudentResponse>> FindById(int id)
        //        {
        //            try
        //            {
        //                return await _studentService.FindById(id);
        //            }
        //            catch (HttpStatusCodeException e)
        //            {
        //                return StatusCode(e.StatusCode, e.Message);
        //            }
        //        }

        /// <summary>
        /// Get Profile of student, for student 
        /// </summary>
        /// <param name="id">Student's id</param>
        /// <returns></returns>
        [Authorize(Roles = Role.Student)]
        [HttpGet("GetProfile/{id}")]
        public async Task<ActionResult<GetStudentProfileResponse>> GetProfile(int id)
        {
            try
            {
                return await _studentService.GetProfile(id);
            }
            catch (HttpStatusCodeException e)
            {
                return StatusCode(e.StatusCode, e.Message);
            }
        }

        /// <summary>
        /// Import List of Student, for admin
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.Admin)]
        [HttpPost]
        public async Task<ActionResult<List<ImportStudentResponse>>> UpdateStudent(List<ImportStudentRequest> requestModel)
        {
            //Check form
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return await _studentService.ImportStudent(requestModel);
            }
            catch (HttpStatusCodeException e)
            {
                return StatusCode(e.StatusCode, e.Message);
            }
        }

        /// <summary>
        /// Update profile for students and authorized users
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.AuthorizedUser + "," + Role.Student)]
        [HttpPut]
        public async Task<ActionResult<UpdateStudentResponse>> UpdateStudent(UpdateStudentRequest requestModel)
        {
            //Check form
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return await _studentService.UpdateStudent(requestModel);
            }
            catch (HttpStatusCodeException e)
            {
                return StatusCode(e.StatusCode, e.Message);
            }
        }

        /// <summary>
        /// Activate/Disable Student for admin
        /// </summary>
        /// <param name="studentId"></param>
        /// /// <param name="status">Target Status</param>
        /// <returns></returns>
        [Authorize(Roles = Role.Admin)]
        [HttpPut("admin/{studentId}/")]
        public async Task<ActionResult<ChangeStudentStatusResponse>> AdminChangeStudentStatus(int studentId, string status)
        {
            try
            {
                if (!UserStatus.IsRole(status))
                {
                    throw new HttpStatusCodeException(400, "Status is not found");
                }

                return await _studentService.ChangeStudentStatus(studentId, status);
            }
            catch (HttpStatusCodeException e)
            {
                return StatusCode(e.StatusCode, e.Message);
            }
        }
    }
}