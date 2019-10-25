using DormyWebService.Entities;
using DormyWebService.Entities.MoneyEntities;

namespace DormyWebService.Repositories.MoneyRepositories
{
    public class RoomMonthlyBillRepository : RepositoryBase<RoomMonthlyBill>, IRoomMonthlyBillRepository
    {
        public RoomMonthlyBillRepository(DormyDbContext context) : base(context)
        {
        }
    }
}