using System;
using System.Threading.Tasks;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Services.UserServices;
using DormyWebService.Utilities;
using DormyWebService.ViewModels.UserModelViews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DormyWebService.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [Authorize(Roles = Role.UnAuthorizedUser + "," + Role.Student)]
        [HttpPut]
        public async Task<ActionResult<Student>> SelfUpdateStudent(UpdateStudentForm requestModel)
        {
            //Check form
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return await _studentService.UpdateSelf(requestModel);
            }
            catch (HttpStatusCodeException e)
            {
                return StatusCode(e.StatusCode, e.Message);
            }
        }
    }
}