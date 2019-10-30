using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using DormyWebService.Entities.ParamEntities;
using DormyWebService.Repositories;
using DormyWebService.Utilities;
using DormyWebService.ViewModels.Param;
using Microsoft.AspNetCore.Mvc;

namespace DormyWebService.Services.ParamServices
{
    public class ParamService : IParamService
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly IMapper _mapper;

        public ParamService(IRepositoryWrapper repoWrapper, IMapper mapper)
        {
            _repoWrapper = repoWrapper;
            _mapper = mapper;
        }

        public async Task<List<Param>> FindAllAsync()
        {
            var result = (List<Param>) await _repoWrapper.Param.FindAllAsync();

            if (!result.Any())
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "There are no param type in database");
            }

            return result;
        }

        public async Task<Param> FindById(int id)
        {
            var result = await _repoWrapper.Param.FindByIdAsync(id);

            if (result == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "Param not found");
            }

            return result;
        }

        public async Task<List<ParamModelView>> FindAllByParamType(int paramTypeId)
        {
            var paramList =
                (List<Param>) await _repoWrapper.Param.FindAllAsyncWithCondition(param =>
                    param.ParamTypeId == paramTypeId);

            if (!paramList.Any() || paramList == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "There are no param type in database");
            }

            return paramList.Select(param => _mapper.Map<ParamModelView>(param)).ToList();
        }

        public async Task<List<Param>> FindAllParamEntitiesByParamType(int paramTypeId)
        {
            var paramList =
                (List<Param>)await _repoWrapper.Param.FindAllAsyncWithCondition(param =>
                    param.ParamTypeId == paramTypeId);

            if (!paramList.Any() || paramList == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "ParamThere are no param type in database");
            }

            return paramList;
        }

        public async Task<List<Param>> FindAllByParamTypeWithoutWarning(int paramTypeId)
        {
            var paramList =
                (List<Param>)await _repoWrapper.Param.FindAllAsyncWithCondition(param =>
                    param.ParamTypeId == paramTypeId);

            return paramList;
        }

        public async Task<bool> IsOfParamType(int paramId, int paramTypeId)
        {
            var tmpParams = (List<Param>) await _repoWrapper.Param.FindAllAsyncWithCondition(p =>
                p.ParamId == paramId && p.ParamTypeId == paramTypeId);

            //Check if room type exists
            return tmpParams.Any();
        }
    }
}