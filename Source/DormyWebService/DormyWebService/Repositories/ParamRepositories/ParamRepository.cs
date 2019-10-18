using DormyWebService.Entities;
using DormyWebService.Entities.ParamEntities;

namespace DormyWebService.Repositories.ParamRepositories
{
    public class ParamRepository : RepositoryBase<Param>, IParamRepository
    {
        public ParamRepository(DormyDbContext context) : base(context)
        {
        }
    }
}