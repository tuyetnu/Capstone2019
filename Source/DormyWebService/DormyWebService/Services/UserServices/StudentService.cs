using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using DormyWebService.ViewModels.UserModelViews.ImportStudent;
using DormyWebService.ViewModels.UserModelViews.UpdateStudent;
using Sieve.Models;
using Sieve.Services;

namespace DormyWebService.Services.UserServices
{
    public class StudentService : IStudentService
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly IUserService _userService;
        private readonly IParamService _paramService;
        private readonly IMapper _mapper;
        private readonly ISieveProcessor _sieveProcessor;

        public StudentService(IRepositoryWrapper repoWrapper, IMapper mapper, IUserService userService,
            IParamService paramService, ISieveProcessor sieveProcessor)
        {
            _repoWrapper = repoWrapper;
            _mapper = mapper;
            _userService = userService;
            _paramService = paramService;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<List<GetAllStudentResponse>> GetAllStudent()
        {
            //Get all student in database
            var students = await _repoWrapper.Student.FindAllAsync();

            //If list is empty, throw exception
            if (!students.Any())
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "No Student is found");
            }

            //Format student list into a list of response object
            var result = students.Select(student => _mapper.Map<GetAllStudentResponse>(student)).ToList();

            return result;
        }

        public async Task<List<GetAllStudentResponse>> AdvancedGetStudent(string sorts, string filters, int? page,
            int? pageSize)
        {
            var sieveModel = new SieveModel()
            {
                Sorts = sorts,
                Page = page,
                PageSize = pageSize,
                Filters = filters,
            };

            var students = await _repoWrapper.Student.FindAllAsync();

            if (students == null || students.Any() == false)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "StudentService: No student is found");
            }

            var sortedStudents = _sieveProcessor.Apply(sieveModel, students.AsQueryable()).ToList();

            return sortedStudents.Select(student => _mapper.Map<GetAllStudentResponse>(student)).ToList();
        }

        public async Task<Student> FindById(int id)
        {
            //Get student in database
            var student = await _repoWrapper.Student.FindByIdAsync(id);

            if (student == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "No Student is found");
            }

            return student;
        }

        public async Task<GetStudentProfileResponse> GetProfile(int id)
        {
            var student = await _repoWrapper.Student.FindByIdAsync(id);

            if (student == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "No Student is found");
            }

            var priorityType = await _paramService.FindById(student.PriorityType);

            var user = await _userService.FindById(student.StudentId);

            return GetStudentProfileResponse.MapFromStudent(student, priorityType, user);
        }

        public async Task<List<ImportStudentResponse>> ImportStudent(List<ImportStudentRequest> requestModel)
        {
            //check if request is empty
            if (!requestModel.Any())
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "StudentService: request is empty");
            }

            //Check records in requestModel for duplicate email
            CheckImportStudentRecords(requestModel);

            var students = new List<Student>();

            //Get all email form database
            var listOfEmail = _repoWrapper.User.FindAllAsync().Result.Select(u => u.Email).ToList();

            foreach (var s in requestModel)
            {
                //Check if Email already existed
                if (listOfEmail.Contains(s.Email))
                {
                    //clear pending changes
                    _repoWrapper.DeleteChanges();
                    throw new HttpStatusCodeException(HttpStatusCode.NotFound,
                        "StudentService: Email: " + s.Email + " Already Existed");
                }

                //Add student to pending changes
                students.Add(_repoWrapper.Student.CreateWithoutSave(ImportStudentRequest.NewStudentFromRequest(s)));
            }

            try
            {
                //Create all students at once
                await _repoWrapper.Save();
            }
            catch (Exception)
            {
                //clear pending changes if fail
                _repoWrapper.DeleteChanges();
                throw new HttpStatusCodeException(HttpStatusCode.InternalServerError,
                    "StudentService: Could not create new student");
            }

            return students.Select(ImportStudentResponse.CreateFromStudent).ToList();
        }

        public async Task<UpdateStudentResponse> UpdateStudent(UpdateStudentRequest requestModel)
        {
            //Find User with the same id in database
            var user = await _userService.FindById(requestModel.StudentId);

            //Check if student already existed in database
            var student = await _repoWrapper.Student.FindByIdAsync(requestModel.StudentId);

            //If student already existed, update student
            student = requestModel.MapToStudent(student);

            //Update Student
            student = await _repoWrapper.Student.UpdateAsync(student, student.StudentId);


            return UpdateStudentResponse.CreateFromStudent(student);
        }

        public async Task<ChangeStudentStatusResponse> ChangeStudentStatus(int id, string status)
        {
            Student student;

            //Find if student exists
            try
            {
                student = await _repoWrapper.Student.FindByIdAsync(id);
            }
            catch (Exception)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "Could not find student in database");
            }

            //Declare User
            var user = await _userService.FindById(id);

            //Change to new status
            user.Status = status;

            //Save changes to user in database
            user = await _repoWrapper.User.UpdateAsync(user, student.StudentId);

            return new ChangeStudentStatusResponse()
            {
                Id = student.StudentId,
                Name = student.Name,
                Status = student.User.Status
            };
        }

        public async Task<bool> HasARoom(int studentId)
        {
            var student = await FindById(studentId);

            return student.RoomId != null;
        }

        private void CheckImportStudentRecords(List<ImportStudentRequest> requestModel)
        {
            //Check if there are duplicate email in request
            foreach (var student in requestModel)
            {
                if (requestModel.Exists(s => s.Email == student.Email))
                {
                    throw new HttpStatusCodeException(HttpStatusCode.NotFound,
                        "StudentService: there are duplicate email of: " + student.Email);
                }
            }
        }
    }
}