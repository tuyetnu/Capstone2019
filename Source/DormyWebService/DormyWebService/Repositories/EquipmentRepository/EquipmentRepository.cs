using DormyWebService.Entities;
using DormyWebService.Entities.EquipmentEntities;

namespace DormyWebService.Repositories.EquipmentRepository
{
    public class EquipmentRepository : RepositoryBase<Equipment>, IEquipmentRepository
    {
        public EquipmentRepository(DormyDbContext context) : base(context)
        {
        }
    }
}