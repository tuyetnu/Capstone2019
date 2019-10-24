using System;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Repositories;
using DormyWebService.Utilities;

namespace DormyWebService.Services.UserServices
{
    public class AdminService : IAdminService
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private IUserService _userService;
        private IMapper _mapper;

        public AdminService(IRepositoryWrapper repoWrapper, IMapper mapper, IUserService userService)
        {
            _repoWrapper = repoWrapper;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<Admin> FindById(int id)
        {
            var result = await _repoWrapper.Admin.FindByIdAsync(id);
            if (result == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "Admin account not found");
            }

            return result;
        }
    }
}