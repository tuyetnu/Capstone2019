using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DormyWebService.Entities.ParamEntities;

namespace DormyWebService.Services.ParamServices
{
    public interface IParamTypeService
    {
        Task<List<ParamType>> FindAllAsync();
        Task<ParamType> FindById(int id);
    }
}
