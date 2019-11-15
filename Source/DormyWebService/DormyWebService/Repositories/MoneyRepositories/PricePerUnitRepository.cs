using DormyWebService.Entities;
using DormyWebService.Entities.MoneyEntities;
using System.Linq;

namespace DormyWebService.Repositories.MoneyRepositories
{
    public class PricePerUnitRepository : RepositoryBase<PricePerUnit>, IPricePerUnitRepository
    {
        public PricePerUnitRepository(DormyDbContext context) : base(context)
        {
        }

        public PricePerUnit FindOneById(int id)
        {
            return Context.PricePerUnits.FirstOrDefault(p => p.TypeId == id);
        }
    }
}