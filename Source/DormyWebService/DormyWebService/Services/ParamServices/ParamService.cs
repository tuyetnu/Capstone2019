using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DormyWebService.Entities.ParamEntities;
using DormyWebService.Repositories;
using DormyWebService.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace DormyWebService.Services.ParamServices
{
    public class ParamService : IParamService
    {
        private IRepositoryWrapper _repoWrapper;

        public ParamService(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }

        public async Task<ICollection<Param>> FindAllAsync()
        {
                var result = (List<Param>)await _repoWrapper.Param.FindAllAsync();
                if (!result.Any())
                {
                    throw new HttpStatusCodeException(404, "There are no param type in database");
                }

                return result;
        }

        public async Task<Param> FindById(int id)
        {
                var result = await _repoWrapper.Param.FindByIdAsync(id);

                if (result == null)
                {
                    throw new HttpStatusCodeException(404, "Param not found");
                }

                return result;
            
        }
    }
}