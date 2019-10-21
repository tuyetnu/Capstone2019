using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Repositories;
using DormyWebService.Services.ParamServices;
using DormyWebService.Utilities;
using DormyWebService.ViewModels.UserModelViews;
using DormyWebService.ViewModels.UserModelViews.ChangeStudentStatus;
using DormyWebService.ViewModels.UserModelViews.GetAllStudent;
using DormyWebService.ViewModels.UserModelViews.GetStudentProfile;
using DormyWebService.ViewModels.UserModelViews.UpdateStudent;

namespace DormyWebService.Services.UserServices
{
    public class StudentService : IStudentService
    {
        private IRepositoryWrapper _repoWrapper;
        private IUserService _userService;
        private IParamService _paramService;
        private IMapper _mapper;

        public StudentService(IRepositoryWrapper repoWrapper, IMapper mapper, IUserService userService, IParamService paramService)
        {
            _repoWrapper = repoWrapper;
            _mapper = mapper;
            _userService = userService;
            _paramService = paramService;
        }

        public async Task<List<GetAllStudentResponse>> GetAllStudent()
        {
            //Get all student in database
            var students =  await _repoWrapper.Student.FindAllAsync();

            //If list is empty, throw exception
            if (!students.Any())
            {
                throw new HttpStatusCodeException(404, "No Student is found");
            }

            //Format student list into a list of response object
            var result = students.Select(student => _mapper.Map<GetAllStudentResponse>(student)).ToList();

            return result;
        }

        public async Task<FindByIdStudentResponse> FindById(int id)
        {
            //Get student in database
            var student = await _repoWrapper.Student.FindByIdAsync(id);

            if (student == null)
            {
                throw new HttpStatusCodeException(404, "No Student is found");
            }

            return _mapper.Map<FindByIdStudentResponse>(student);
        }

        public async Task<GetStudentProfileResponse> GetProfile(int id)
        {
            var student = await _repoWrapper.Student.FindByIdAsync(id);

            if (student == null)
            {
                throw new HttpStatusCodeException(404, "No Student is found");
            }

            var priorityType = await _paramService.FindById(student.PriorityType);

            return GetStudentProfileResponse.MapFromStudent(student, priorityType);
        }

        public async Task<UpdateStudentResponse> UpdateStudent(UpdateStudentRequest requestModel)
        {
//            System.Diagnostics.Debug.WriteLine("Went to service");

            //Find User with the same id in database
            var user = await _userService.FindById(requestModel.StudentId);

            //Check if student already existed in database
            var student = await _repoWrapper.Student.FindByIdAsync(requestModel.StudentId);

            //If there isn't a student with this id, create new
            if (student == null)
            {
                student = _mapper.Map<Student>(requestModel);
//                System.Diagnostics.Debug.WriteLine("student.Id:" + student.StudentId);
//                System.Diagnostics.Debug.WriteLine("student.Name:" + student.Name);
                student.IsRoomLeader = false;
                student.AccountBalance = 0;

                System.Diagnostics.Debug.WriteLine("student: " + student);

                try
                {
                    student = await _repoWrapper.Student.CreateAsync(student);
                }
                catch (Exception)
                {
                    throw new HttpStatusCodeException(500, "Could not create new student");
                }

                user.Role = Role.Student;
                await _repoWrapper.User.UpdateAsync(user, user.UserId);
            }
            else
            {
                student = requestModel.MapToStudent(student);

                try
                {
                    student = await _repoWrapper.Student.UpdateAsync(student, student.StudentId);
                }
                catch (Exception)
                {
                    throw new HttpStatusCodeException(500, "Failed to update student");
                }
                

            }

            var result = _mapper.Map<UpdateStudentResponse>(student);
            return result;
        }

        public async Task<ChangeStudentStatusResponse> ChangeStudentStatus(int id, string status)
        {
//            System.Diagnostics.Debug.WriteLine("ChangeStudentStatus: id: " + id);
//            System.Diagnostics.Debug.WriteLine("ChangeStudentStatus: status: " + status);

            Student student;

            try
            {
                student = await _repoWrapper.Student.FindByIdAsync(id);
            }
            catch (Exception)
            {
                throw new HttpStatusCodeException(404, "Could not find student in database");
            }

//            System.Diagnostics.Debug.WriteLine("ChangeStudentStatus: Found Student ");

            //Declare User
            var user = await _userService.FindById(id);

            //Change to new status
            user.Status = status;

            //Save changes to user in database
            try
            {
                user = await _repoWrapper.User.UpdateAsync(user, student.StudentId);
            }
            catch (Exception)
            {
                throw new HttpStatusCodeException(500, "Failed to update student's status'");
            }
            
            return new ChangeStudentStatusResponse()
            {
                Id = student.StudentId,
                Name = student.Name,
                Status = student.User.Status
            };
        }
    }
}