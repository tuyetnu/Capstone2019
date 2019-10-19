﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DormyWebService.Entities;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.ParamEntities;
using DormyWebService.Services.ParamServices;
using DormyWebService.Utilities;
using Microsoft.AspNetCore.Authorization;

namespace DormyWebService.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ParamTypesController : ControllerBase
    {
        private IParamTypeService _paramTypeService;

        public ParamTypesController(IParamTypeService paramTypeService)
        {
            _paramTypeService = paramTypeService;
        }

        /// <summary>
        /// See how many Param Types there are, have to be logged in to see
        /// </summary>
        /// <remarks>authorization disabled for debug purposes</remarks>
        /// See how many Param Types there are, have to be logged in to see
        /// </remarks>
//        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        public async Task<ActionResult<List<ParamType>>> GetAllParamTypes()
        {
            try
            {
                return (List<ParamType>)await _paramTypeService.FindAllAsync();
            }
            catch (HttpStatusCodeException e)
            {
                return StatusCode(e.StatusCode, e.Message);
            }
        }

        /// <summary>
        /// Find Param Type by Id, have to be logged in to see
        /// </summary>
        /// <remarks>authorization disabled for debug purposes</remarks>
        /// //        [Authorize(Roles = Role.Admin)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ParamType>> FindById(int id)
        {
            try
            {
                return await _paramTypeService.FindById(id);
            }
            catch (HttpStatusCodeException e)
            {
                return StatusCode(e.StatusCode, e.Message);
            }
        }
    }
}
