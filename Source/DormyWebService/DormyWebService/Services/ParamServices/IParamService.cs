using System.Collections.Generic;
using System.Threading.Tasks;
using DormyWebService.Entities.ParamEntities;
using DormyWebService.ViewModels.Param;
using Microsoft.AspNetCore.Mvc;

namespace DormyWebService.Services.ParamServices
{
    public interface IParamService
    {
        Task<List<Param>> FindAllAsync();
        Task<Param> FindById(int id);

        Task<List<ParamModelView>> FindAllByParamType(int paramTypeId);
        Task<List<Param>> FindAllByParamTypeWithoutWarning(int paramTypeId);

        Task<bool> IsOfParamType(int paramId, int paramTypeId);
    }
}