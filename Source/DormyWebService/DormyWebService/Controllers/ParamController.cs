using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DormyWebService.Services.ParamServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DormyWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParamController : ControllerBase
    {
        private IParamService _paramService;

        public ParamController(IParamService paramService)
        {
            _paramService = paramService;
        }
    }
}