using DormyWebService.Entities;
using DormyWebService.Entities.ContractEntities;

namespace DormyWebService.Repositories.ContractRepositories
{
    public class ContractRepository : RepositoryBase<Contract>, IContractRepository
    {
        public ContractRepository(DormyDbContext context) : base(context)
        {
        }
    }
}