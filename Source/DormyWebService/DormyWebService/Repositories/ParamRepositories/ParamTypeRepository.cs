using DormyWebService.Entities;
using DormyWebService.Entities.ParamEntities;

namespace DormyWebService.Repositories.ParamRepositories
{
    public class ParamTypeRepository : RepositoryBase<ParamType>, IParamTypeRepository
    {
        public ParamTypeRepository(DormyDbContext context) : base(context)
        {
        }
    }
}