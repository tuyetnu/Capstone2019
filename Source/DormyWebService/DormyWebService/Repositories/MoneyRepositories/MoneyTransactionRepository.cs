using DormyWebService.Entities;
using DormyWebService.Entities.MoneyEntities;

namespace DormyWebService.Repositories.MoneyRepositories
{
    public class MoneyTransactionRepository : RepositoryBase<MoneyTransaction>, IMoneyTransactionRepository
    {
        public MoneyTransactionRepository(DormyDbContext context) : base(context)
        {
        }
    }
}