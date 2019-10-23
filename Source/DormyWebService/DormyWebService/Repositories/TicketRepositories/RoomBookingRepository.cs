using DormyWebService.Entities;
using DormyWebService.Entities.TicketEntities;

namespace DormyWebService.Repositories.TicketRepositories
{
    public class RoomBookingRepository : RepositoryBase<RoomBookingRequestForm>, IRoomBookingRepository
    {
        public RoomBookingRepository(DormyDbContext context) : base(context)
        {
        }
    }
}