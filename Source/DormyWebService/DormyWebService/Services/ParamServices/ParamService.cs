using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DormyWebService.Entities.ParamEntities;
using DormyWebService.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DormyWebService.Services.ParamServices
{
    public class ParamService : IParamService
    {
        private IRepositoryWrapper _repoWrapper;
        public async Task<ICollection<Param>> FindAllAsync()
        {
            return await _repoWrapper.Param.FindAllAsync();
        }
    }
}