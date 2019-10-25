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

            return new HomeResponse()
            {
                IsHaveRoom = student.RoomId == null,
                NumberOfUnseenNotification = 0,
                IsHaveRequestRenew = false,
                IsHaveRequestBooking = roomBookings.Any() ,
                IsHaveRequestCancel = false,
                IsHaveRequestTransfer = false,
                IsHavePayment = false
            };

        }
    }
}