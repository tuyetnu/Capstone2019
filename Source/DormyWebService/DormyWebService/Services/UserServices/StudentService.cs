using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Repositories;
using DormyWebService.Utilities;
using DormyWebService.ViewModels.UserModelViews;

namespace DormyWebService.Services.UserServices
{
    public class StudentService : IStudentService
    {
        private IRepositoryWrapper _repoWrapper;
        private IMapper _mapper;

        public StudentService(IRepositoryWrapper repoWrapper, IMapper mapper)
        {
            _repoWrapper = repoWrapper;
            _mapper = mapper;
    }

        public async Task<ICollection<Student>> GetAllStudent()
        {
            var result = (List<Student>) await _repoWrapper.Student.FindAllAsync();

            if (!result.Any())
            {
                throw new HttpStatusCodeException(404, "No Student is found");
            }

            return result;
        }

        public async Task<Student> FindById(int id)
        {
            var result = await _repoWrapper.Student.FindByIdAsync(id);

            if (result == null)
            {
                throw new HttpStatusCodeException(404, "No Student is found");
            }

            return result;
        }

        public async Task<Student> UpdateSelf(UpdateStudentForm requestModel)
        {
            //Check if there's this user in database
            if (await _repoWrapper.User.FindByIdAsync(requestModel.Id) == null)
            {
                throw new HttpStatusCodeException(404, "User Profile is not found");
            }

            var student = await _repoWrapper.Student.FindByIdAsync(requestModel.Id);

            //If there isn't a student with this id, create new
            if (student == null)
            {
                student = _mapper.Map<Student>(requestModel);

                student.IsRoomLeader = false;
                student.AccountBalance = 0;

                System.Diagnostics.Debug.WriteLine("student: " + student);

                student = await _repoWrapper.Student.CreateAsync(student);

                if (student == null)
                {
                    throw new HttpStatusCodeException(500, "Could not create new student");
                }
            }
            else
            {
                student.Name = requestModel.Name;
                student.Address = requestModel.Address;
                student.StartedSchoolYear = requestModel.StartedSchoolYear;
                student.Term = requestModel.Term;
                student.IdentityNumber = requestModel.IdentityNumber;
                student.StudentCardNumber = requestModel.StudentCardNumber;
                student.PriorityType = requestModel.PriorityType;
                student.Gender = requestModel.Gender;

                student = await _repoWrapper.Student.UpdateAsync(student, student.StudentId);

                if (student == null)
                {
                    throw new HttpStatusCodeException(500, "Could not create new student");
                }
            }

            return student;
        }
    }
}