using DormyWebService.Entities.MoneyEntities;

namespace DormyWebService.Repositories.MoneyRepositories
{
    public interface IPricePerUnitRepository : IRepository<PricePerUnit>
    {
        PricePerUnit FindOneById(int id);
    }
}