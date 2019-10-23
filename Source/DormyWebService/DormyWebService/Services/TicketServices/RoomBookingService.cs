using System;
using System.Threading.Tasks;
using AutoMapper;
using DormyWebService.Entities.TicketEntities;
using DormyWebService.Repositories;
using DormyWebService.Utilities;
using Sieve.Services;

namespace DormyWebService.Services.TicketServices
{
    public class RoomBookingService : IRoomBookingService
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private IMapper _mapper;
        private ISieveProcessor _sieveProcessor;

        public RoomBookingService(IRepositoryWrapper repoWrapper, IMapper mapper, ISieveProcessor sieveProcessor)
        {
            _repoWrapper = repoWrapper;
            _mapper = mapper;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<RoomBookingRequestForm> FindById(int id)
        {
            RoomBookingRequestForm result;
            try
            {
                result = await _repoWrapper.RoomBooking.FindByIdAsync(id);
            }
            catch (Exception)
            {
                throw new HttpStatusCodeException(500, "RoomBookingService: Internal Server Error Occured when finding Room Booking");
            }

            if (result == null)
            {
                throw new HttpStatusCodeException(404, "RoomBookingService: Room Booking is not found");
            }

            return result;
        }
    }
}