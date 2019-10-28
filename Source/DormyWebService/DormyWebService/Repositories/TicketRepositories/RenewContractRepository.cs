using DormyWebService.Entities;
using DormyWebService.Entities.TicketEntities;

namespace DormyWebService.Repositories.TicketRepositories
{
    public class RenewContractRepository : RepositoryBase<ContractRenewalForm>, IRenewContractRepository
    {
        public RenewContractRepository(DormyDbContext context) : base(context)
        {
        }
    }
}