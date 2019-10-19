using System.Collections.Generic;
using System.Threading.Tasks;
using DormyWebService.Entities.ParamEntities;
using DormyWebService.Repositories;

namespace DormyWebService.Services.ParamServices
{
    public class ParamTypeService : IParamTypeService
    {
        private IRepositoryWrapper _repoWrapper;

        public ParamTypeService(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }

        public async Task<ICollection<ParamType>> FindAllAsync()
        {
            return await _repoWrapper.ParamType.FindAllAsync();
        }

        public async Task<ParamType> FindById(int id)
        {
            return await _repoWrapper.ParamType.FindByIdAsync(id);
        }
    }
}