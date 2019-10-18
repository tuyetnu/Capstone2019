using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DormyWebService.Entities;
using DormyWebService.Entities.ParamEntities;

namespace DormyWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParamTypesController : ControllerBase
    {
        private readonly DormyDbContext _context;

        public ParamTypesController(DormyDbContext context)
        {
            _context = context;
        }
    }
}
