using DormyWebService.Entities;
using DormyWebService.Entities.TicketEntities;

namespace DormyWebService.Repositories.TicketRepositories
{
    public class CancelContractRepository : RepositoryBase<CancelContractForm>, ICancelContractRepository
    {
        public CancelContractRepository(DormyDbContext context) : base(context)
        {
        }
    }
}