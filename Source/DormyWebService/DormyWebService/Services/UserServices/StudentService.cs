using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.ContractEntities;
using DormyWebService.Entities.RoomEntities;
using DormyWebService.Entities.TicketEntities;
using DormyWebService.Repositories;
using DormyWebService.Services.ParamServices;
using DormyWebService.Utilities;
using DormyWebService.ViewModels.UserModelViews;
using DormyWebService.ViewModels.UserModelViews.ChangeStudentStatus;
using DormyWebService.ViewModels.UserModelViews.CheckStudentForRenewContract;
using DormyWebService.ViewModels.UserModelViews.GetAllStudent;
using DormyWebService.ViewModels.UserModelViews.GetStudentProfile;
using DormyWebService.ViewModels.UserModelViews.GetStudentRequestedList;
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

        /// <summary>
        /// Get all students in database
        /// </summary>
        /// <returns></returns>
        public async Task<List<GetAllStudentResponse>> GetAllStudent()
        {
            //Get all student in dorm (đã ở)
            var students = await _repoWrapper.Student.FindAllAsyncWithCondition(s => s.RoomId != null && !s.IsHold);

            //If list is empty, throw exception
            if (!students.Any())
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "No Student is found");
            }

            //Format student list into a list of response object
            var result = students.Select(student => _mapper.Map<GetAllStudentResponse>(student)).ToList();

            return result;
        }

        /// <summary>
        /// Get list of students with condition
        /// </summary>
        /// <param name="sorts"></param>
        /// <param name="filters"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
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

            var students = await _repoWrapper.Student.FindAllAsyncWithCondition( s => s.RoomId != null && !s.IsHold);
         
            foreach (var student in students)
            {
                var contract = await _repoWrapper.Contract.FindAllAsyncWithCondition(c => c.StudentId == student.StudentId);
            }

            if (students == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "StudentService: No student is found");
            }

            var sortedStudents = _sieveProcessor.Apply(sieveModel, students.AsQueryable()).ToList();

            return sortedStudents.Select(student => _mapper.Map<GetAllStudentResponse>(student)).ToList();
        }

        /// <summary>
        /// Performs general checks if student can renew contract for front end
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CheckStudentForRenewContractResponse> CheckStudentForRenewContract(int id)
        {
            //Find Student, including checking if student is found or not
            var student = _repoWrapper.Student.FindWithRoomBydId(id);
            var contracts = (await _repoWrapper.Contract.FindByAsync(c=> c.StudentId == id && c.Status == ContractStatus.Active)).FirstOrDefault();
                return new CheckStudentForRenewContractResponse
                {
                    HasInValidTrainingPoint = await CheckEvaluationScoreForRenewContract(student),
                    HasStayedMoreThanPermittedYear = await CheckMaxYearForStayingForRenewContract(student),
                    ContractIsActiveNextMonth = await CheckContractNextMonthForRenewContract(student),
                    NumberOfRoomTransferRequest = await GetNumberOfRoomTransferRequest(student),
                    SystemDate = DateTime.Now.AddHours(GlobalParams.TimeZone).ToString(GlobalParams.BirthDayFormat),
                    RoomName = student.Room.Name,
                    RoomType = student.Room.RoomType,
                    ContractEndDate = contracts.EndDate.ToString(GlobalParams.BirthDayFormat),
                };
        }

        private async Task<int> GetNumberOfRoomTransferRequest(Student student)
        {
            var result = 0;

            var transferRequests = (List<RoomTransferRequestForm>) await 
                _repoWrapper.RoomTransfer.FindAllAsyncWithCondition(r => r.StudentId == student.StudentId);

            if (transferRequests != null)
            {
                result = transferRequests.Count;
            }

            return result;
        }

        /// <summary>
        /// Check if a student's contract is active next month
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        private async Task<bool> CheckContractNextMonthForRenewContract(Student student)
        {
            var result = false;
            //Get current active contract of user
            var contracts = (List<Contract>) await _repoWrapper.Contract.FindAllAsyncWithCondition(c => c.StudentId == student.StudentId && c.Status == ContractStatus.Active);

            if (contracts != null && contracts.Count > 0)
            {
                var now = DateTime.Now.AddHours(GlobalParams.TimeZone);
                result = now.AddMonths(1) <= contracts[0].EndDate;
            }

            return result;
        }

        /// <summary>
        /// Check if student have staying for too long in dormitory
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        private async Task<bool> CheckMaxYearForStayingForRenewContract(Student student)
        {
            //Get max year for students to be staying
            var maxYearForStaying = await
                _paramService.FindById(GlobalParams.ParamMaxYearForStaying);

            //Get student's startdate, beginning at September
            var startDate = new DateTime(student.StartedSchoolYear, 9, 1);
            // student's startdate = maxYearForStaying
            if (maxYearForStaying.Value != null)
            {
                var maxYear = startDate.AddYears(maxYearForStaying.Value.Value);
                // Get Now Time
                var now = DateTime.Now.AddHours(GlobalParams.TimeZone);

                return now > maxYear;
            }

            return false;
        }

        /// <summary>
        /// Check if student has valid evaluation score for renewing contract
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        private async Task<bool> CheckEvaluationScoreForRenewContract(Student student)
        {
            //Get get minimum evaluation score for student to be considered invalid
            var contractRenewalEvaluationScoreMargin = await
                _paramService.FindById(GlobalParams.ParamContractRenewalEvaluationPointMargin);

            return student.EvaluationScore < contractRenewalEvaluationScoreMargin.Value;
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

        /// <summary>
        /// Get student's profile for viewing
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<GetStudentProfileResponse> GetProfile(int id)
        {
            var student = await _repoWrapper.Student.FindByIdAsync(id);

            if (student == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "No Student is found");
            }

            var priorityType = await _paramService.FindById(student.PriorityType);

            var user = await _userService.FindById(student.StudentId);

            Room room = null;
            if (student.RoomId != null)
            {
                room = await _repoWrapper.Room.FindByIdAsync(student.RoomId.Value);
            }

            return GetStudentProfileResponse.MapFromStudent(student, priorityType, user, room);
        }

        /// <summary>
        /// Import a list of students into database
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public async Task<List<ImportStudentResponse>> ImportStudent(List<ImportStudentRequest> requestModel)
        {
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
                    throw new Exception("Email: " + s.Email + " đã tồn tại");
                }

                var defaultEvaluationPoint = (await _paramService.FindById(GlobalParams.ParamDefaultEvaluationPoint))?.Value ?? GlobalParams.DefaultEvaluationPoint;

                //Add student to pending changes
                students.Add(_repoWrapper.Student.CreateWithoutSave(ImportStudentRequest.NewStudentFromRequest(s, defaultEvaluationPoint)));
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
                //throw new HttpStatusCodeException(HttpStatusCode.InternalServerError,
                //    "StudentService: Could not create new student");
                throw new Exception("500");
            }

            return students.Select(ImportStudentResponse.CreateFromStudent).ToList();
        }

        /// <summary>
        /// Update student's information
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
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
                if (requestModel.FindAll(s => s.Email == student.Email).Count >= 2)
                {
                    throw new HttpStatusCodeException(HttpStatusCode.NotFound,
                        "StudentService: there are duplicate email of: " + student.Email);
                }
            }
        }

        public async Task<bool> AutoResetEvaluationPoint()
        {
            //Get default training point to know what to reset point to
            var defaultEvaluationPoint = await _paramService.FindById(GlobalParams.ParamDefaultEvaluationPoint);

            var activeStudents = (List<Student>)
                await _repoWrapper.Student.FindAllAsyncWithCondition(s => s.User.Status == UserStatus.Active);

            //if students are found
            if (activeStudents != null && activeStudents.Any())
            {
                //go through every active students
                foreach (var student in activeStudents)
                {
                    if (defaultEvaluationPoint.Value != null)
                    {
                        student.EvaluationScore = defaultEvaluationPoint.Value.Value;
                        await _repoWrapper.Student.UpdateAsyncWithoutSave(student, student.StudentId);
                    }
                }

                //Save all changes
                await _repoWrapper.Save();
            }

            return true;
        }

        public async Task<bool> ResetEvaluationPoint()
        {
            //Get default training point to know what to reset point to
            var defaultEvaluationPoint = await _paramService.FindById(GlobalParams.ParamDefaultEvaluationPoint);

            var activeStudents =(List<Student>)
                await _repoWrapper.Student.FindAllAsyncWithCondition(s => s.User.Status == UserStatus.Active);

            //if students are found
            if (activeStudents != null && activeStudents.Any())
            {
                //go through every active students
                foreach (var student in activeStudents)
                {
                    if (defaultEvaluationPoint.Value != null)
                    {
                        student.EvaluationScore = defaultEvaluationPoint.Value.Value;
                        await _repoWrapper.Student.UpdateAsyncWithoutSave(student, student.StudentId);
                    }
                }

                //Save all changes
                await _repoWrapper.Save();
            }

            return true;
        }

        public async Task<List<StudentRequestResponse>> GetAllStudentRequestById(int studentId)
        {
            //throw new NotImplementedException();
            var temps = new List<StudentRequestResponseTemp>();
            var bookRoomList = (await _repoWrapper.RoomBooking.FindAllAsyncWithCondition(s => s.StudentId == studentId)).ToList();
            if(bookRoomList != null)
            {
                foreach(RoomBookingRequestForm roomBooking in bookRoomList)
                {
                    temps.Add(new StudentRequestResponseTemp("Booking", roomBooking.RoomBookingRequestFormId, roomBooking.Status, roomBooking.CreatedDate, roomBooking.LastUpdated));
                }
            }
            var bookTransferList = (await _repoWrapper.RoomTransfer.FindAllAsyncWithCondition(s => s.StudentId == studentId)).ToList();
            if (bookTransferList != null)
            {
                foreach (RoomTransferRequestForm roomTransfer in bookTransferList)
                {
                    temps.Add(new StudentRequestResponseTemp("Transfer", roomTransfer.RoomTransferRequestFormId, roomTransfer.Status, roomTransfer.CreatedDate, roomTransfer.LastUpdated));
                }
            }
            var renewRequestList = (await _repoWrapper.RenewContract.FindAllAsyncWithCondition(s => s.StudentId == studentId)).ToList();
            if (bookTransferList != null)
            {
                foreach (RoomTransferRequestForm roomTransfer in bookTransferList)
                {
                    temps.Add(new StudentRequestResponseTemp("Renew", roomTransfer.RoomTransferRequestFormId, roomTransfer.Status, roomTransfer.CreatedDate, roomTransfer.LastUpdated));
                }
            }
            var cancelRequestList = (await _repoWrapper.CancelContract.FindAllAsyncWithCondition(s => s.StudentId == studentId)).ToList();
            if (bookTransferList != null)
            {
                foreach (RoomTransferRequestForm roomTransfer in bookTransferList)
                {
                    temps.Add(new StudentRequestResponseTemp("Cancel", roomTransfer.RoomTransferRequestFormId, roomTransfer.Status, roomTransfer.CreatedDate, roomTransfer.LastUpdated));
                }
            }
            temps.Sort((x, y) => y.createDate.CompareTo(x.createDate));
            var respone = new List<StudentRequestResponse>();
            foreach (StudentRequestResponseTemp temp in temps)
            {
                var responeElement = new StudentRequestResponse(temp);
                respone.Add(responeElement);
            }
            return respone;

        }
    }
}