using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DormyWebService.Entities.ParamEntities;
using DormyWebService.Repositories;
using DormyWebService.Utilities;

namespace DormyWebService.Services.ParamServices
{
    public class ParamTypeService : IParamTypeService
    {
        private readonly IRepositoryWrapper _repoWrapper;

        public ParamTypeService(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }

        public async Task<List<ParamType>> FindAllAsync()
        {
            var result = (List<ParamType>) await _repoWrapper.ParamType.FindAllAsync();

            return result;
        }

        public async Task<ParamType> FindById(int id)
        {
            return await _repoWrapper.ParamType.FindByIdAsync(id);
        }
    }
}