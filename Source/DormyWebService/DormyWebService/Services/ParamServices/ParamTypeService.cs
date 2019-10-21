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
        private IRepositoryWrapper _repoWrapper;

        public ParamTypeService(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }

        public async Task<List<ParamType>> FindAllAsync()
        {
            List<ParamType> result;
            try
            {
                result = (List<ParamType>) await _repoWrapper.ParamType.FindAllAsync();
            }
            catch (Exception)
            {
                throw new HttpStatusCodeException(500, "Internal Server Error when attempting to get ParamType from Database");
            }

            return result;
        }

        public async Task<ParamType> FindById(int id)
        {
            return await _repoWrapper.ParamType.FindByIdAsync(id);
        }
    }
}