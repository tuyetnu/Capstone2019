using DormyWebService.Entities;
using DormyWebService.Entities.TicketEntities;

namespace DormyWebService.Repositories.TicketRepositories
{
    public class RoomTransferRepository : RepositoryBase<RoomTransferRequestForm>, IRoomTransferRepository
    {
        public RoomTransferRepository(DormyDbContext context) : base(context)
        {
        }
    }
}