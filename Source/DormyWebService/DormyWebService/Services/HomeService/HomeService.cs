using System;
using System.Net;
using System.Threading.Tasks;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.TicketEntities;
using DormyWebService.Repositories;
using DormyWebService.Services.NewFolder;
using DormyWebService.Services.UserServices;
using DormyWebService.Utilities;
using DormyWebService.ViewModels.HomeModelView;
using Microsoft.EntityFrameworkCore.Internal;

namespace DormyWebService.Services.HomeService
{
    public class HomeService : IHomeService
    {
        private IRepositoryWrapper _repoWrapper;
        private IStudentService _studentService;

        public HomeService(IStudentService studentService, IRepositoryWrapper repoWrapper)
        {
            _studentService = studentService;
            _repoWrapper = repoWrapper;
        }

        public async Task<HomeResponse> GetInitialValues(int studentId)
        {
            var student = await _repoWrapper.Student.FindByIdAsync(studentId);

            if (student == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "HomeService: Student not found");
            }
            var roomBookings =await _repoWrapper.RoomBooking.FindAllAsyncWithCondition(r => r.StudentId == studentId && (r.Status == RequestStatus.Pending || r.Status == RequestStatus.Approved));
            var roomTransfer = await _repoWrapper.RoomTransfer.FindAllAsyncWithCondition(r => r.StudentId == studentId && (r.Status == RequestStatus.Pending || r.Status == RequestStatus.Approved));
            var renewContract = await _repoWrapper.RenewContract.FindAllAsyncWithCondition(r => r.StudentId == studentId && (r.Status == RequestStatus.Pending || r.Status == RequestStatus.Approved));
            var cancelContract = await _repoWrapper.CancelContract.FindAllAsyncWithCondition(r => r.StudentId == studentId && (r.Status == RequestStatus.Pending));

            return new HomeResponse()
            {
                StudentId = student.StudentId,
                StudentName = student.Name,
                IsHaveRoom = student.RoomId != null,
                NumberOfUnseenNotification = 0,
                IsHaveRequestRenew = renewContract.Any(),
                IsHaveRequestBooking = roomBookings.Any() ,
                IsHaveRequestCancel = cancelContract.Any(),
                IsHaveRequestTransfer = roomTransfer.Any(),
                IsHavePayment = false,
                SystemDate = DateTime.Now.AddHours(GlobalParams.TimeZone).ToString(GlobalParams.BirthDayFormat),
            };

        }
    }
}