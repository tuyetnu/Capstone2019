using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using DormyWebService.Entities.TicketEntities;
using DormyWebService.Repositories;
using DormyWebService.Services.ParamServices;
using DormyWebService.Services.UserServices;
using DormyWebService.Utilities;
using DormyWebService.ViewModels.TicketViewModels.RoomTransfer.SendRoomTransfer;
using Sieve.Services;

namespace DormyWebService.Services.TicketServices
{
    public class RoomTransferService : IRoomTransferService
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private IMapper _mapper;
        private ISieveProcessor _sieveProcessor;
        private readonly IStudentService _studentService;
        private readonly IParamService _paramService;
        private readonly IUserService _userService;

        public RoomTransferService(IRepositoryWrapper repoWrapper, IMapper mapper, ISieveProcessor sieveProcessor, IStudentService studentService, IParamService paramService, IUserService userService)
        {
            _repoWrapper = repoWrapper;
            _mapper = mapper;
            _sieveProcessor = sieveProcessor;
            _studentService = studentService;
            _paramService = paramService;
            _userService = userService;
        }

        public async Task<SendRoomTransferRespone> SendRequest(SendRoomTransferRequest request)
        {
            //Check if room exist
            var room = await _repoWrapper.Room.FindByIdAsync(request.RoomId);

            if (room == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "RoomTransferService: Room not found");
            }

            //Find student in database
            await _studentService.FindById(request.StudentId);

            //Check if there's active room transfer request
            //Chỗ này ko phải _repoWrapper.RoomBooking mà là _repoWrapper.RoomTransfer
            var bookings = (List<RoomTransferRequestForm>)
                await _repoWrapper.RoomTransfer.FindAllAsyncWithCondition(r => r.StudentId == request.StudentId);
            if (bookings!= null)
            {
                if (bookings.Exists(b => b.Status == RequestStatus.Pending))
                {
                    throw new HttpStatusCodeException(HttpStatusCode.Forbidden, "RoomTransferService: There are already active transfer requests for this account");
                }
            }

            //TODO: check if contract is valid

            //Create new room booking from request
            var result = SendRoomTransferRequest.NewEntityFromRequest(request);

            //Create in database
            result = await _repoWrapper.RoomTransfer.CreateAsync(result);
            return new SendRoomTransferRespone()
            {
                RoomTransferFormId = result.RoomTransferRequestFormId
            };
        }
    }
}