using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DormyWebService.Entities.ParamEntities;
using DormyWebService.Repositories;
using DormyWebService.Utilities;
using DormyWebService.ViewModels.UserModelViews.Param;
using Microsoft.AspNetCore.Mvc;

namespace DormyWebService.Services.ParamServices
{
    public class ParamService : IParamService
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private IMapper _mapper;

        public ParamService(IRepositoryWrapper repoWrapper, IMapper mapper)
        {
            _repoWrapper = repoWrapper;
            _mapper = mapper;
        }

        public async Task<List<Param>> FindAllAsync()
        {
            List<Param> result;

            try
            {
                result = (List<Param>)await _repoWrapper.Param.FindAllAsync();
            }
            catch (Exception )
            {
                throw new HttpStatusCodeException(500, "Internal Server Error when attempting to get Param from Database");
            }

            if (!result.Any())
            {
                throw new HttpStatusCodeException(404, "There are no param type in database");
            }

            return result;
        }

        public async Task<Param> FindById(int id)
        {
            Param result;
            try
            {
                result = await _repoWrapper.Param.FindByIdAsync(id);
            }
            catch (Exception)
            {
                throw new HttpStatusCodeException(500, "Internal Server Error when attempting to get Param from Database");
            }

            if (result == null)
            {
                throw new HttpStatusCodeException(404, "Param not found");
            }

            return result;
            
        }

        public async Task<List<ParamModelView>> FindAllByParamType(int paramTypeId)
        {
            List<Param> paramList;

            try
            {
                paramList = (List<Param>)await _repoWrapper.Param.FindAllAsyncWithCondition(param => param.ParamTypeId == paramTypeId);
            }
            catch (Exception)
            {
                throw new HttpStatusCodeException(500, "Internal Server Error when attempting to get Param from Database");
            }

            if (!paramList.Any())
            {
                throw new HttpStatusCodeException(404, "There are no param type in database");
            }

            return paramList.Select(param => _mapper.Map<ParamModelView>(param)).ToList();
        }
    }
}