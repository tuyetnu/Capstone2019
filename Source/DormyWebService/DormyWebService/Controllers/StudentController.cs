using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Services.UserServices;
using DormyWebService.Utilities;
using DormyWebService.ViewModels.UserModelViews;
using DormyWebService.ViewModels.UserModelViews.ChangeStudentStatus;
using DormyWebService.ViewModels.UserModelViews.CheckStudentForRenewContract;
using DormyWebService.ViewModels.UserModelViews.GetAllStudent;
using DormyWebService.ViewModels.UserModelViews.GetStudentProfile;
using DormyWebService.ViewModels.UserModelViews.GetStudentRequestedList;
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
            return await _studentService.GetAllStudent();
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
        public async Task<ActionResult<List<GetAllStudentResponse>>> AdvancedGetStudent(string sorts, string filters,
            int? page, int? pageSize)
        {
            return await _studentService.AdvancedGetStudent(sorts, filters, page, pageSize);
        }

        /// <summary>
        /// Get Profile of student, for student 
        /// </summary>
        /// <param name="id">Student's id</param>
        /// <returns></returns>
        [Authorize(Roles = Role.Student)]
        [HttpGet("GetProfile/{id}")]
        public async Task<ActionResult<GetStudentProfileResponse>> GetProfile(int id)
        {
            return await _studentService.GetProfile(id);
        }

        /// <summary>
        /// Check if student can Renew Contract, for authorized users
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("CheckStudentForRenewContract/{id}")]
        public async Task<ActionResult<CheckStudentForRenewContractResponse>>
            CheckStudentForRenewContractResponse(int id)
        {
            return await _studentService.CheckStudentForRenewContract(id);
        }

        /// <summary>
        /// Import List of Student, for admin
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.Admin)]
        [HttpPost]
        public async Task<ActionResult<List<ImportStudentResponse>>> UpdateStudent(
            List<ImportStudentRequest> requestModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return await _studentService.ImportStudent(requestModel);
            }
            catch (Exception ex)
            {
                if (ex.Message == "500")
                {
                    return StatusCode(500);
                } else
                {
                    return BadRequest(ex.Message);
                }
            }
            
        }

        /// <summary>
        /// Update profile for students and authorized student
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.Student)]
        [HttpPut]
        public async Task<ActionResult<UpdateStudentResponse>> UpdateStudent(UpdateStudentRequest requestModel)
        {
            //Check form
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return await _studentService.UpdateStudent(requestModel);
        }

        /// <summary>
        /// Activate/Disable Student for admin
        /// </summary>
        /// <param name="studentId"></param>
        /// /// <param name="status">Target Status</param>
        /// <returns></returns>
        [Authorize(Roles = Role.Admin)]
        [HttpPut("admin/{studentId}/")]
        public async Task<ActionResult<ChangeStudentStatusResponse>> AdminChangeStudentStatus(int studentId,
            string status)
        {
            if (!UserStatus.IsRole(status))
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "Status is not found");
            }

            return await _studentService.ChangeStudentStatus(studentId, status);
        }

        [Authorize(Roles = Role.Admin + "," + Role.Staff + "," + Role.Student)]
        [HttpGet("GetAllRequest/{studentId}")]
        public async Task<ActionResult<List<StudentRequestResponse>>> GetAllStudentRequest(int studentId)
        {
            return await _studentService.GetAllStudentRequestById(studentId);
        }
    }
}