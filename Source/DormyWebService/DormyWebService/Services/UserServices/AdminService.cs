using System;
using System.Threading.Tasks;
using AutoMapper;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Repositories;
using DormyWebService.Utilities;

namespace DormyWebService.Services.UserServices
{
    public class AdminService : IAdminService
    {
        private IRepositoryWrapper _repoWrapper;
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
            Admin result;
            try
            {
                result = await _repoWrapper.Admin.FindByIdAsync(id);
            }
            catch (Exception)
            {
                throw new HttpStatusCodeException(500, "Internal Server Error when finding admin in database");
            }
            if (result == null)
            {
                throw new HttpStatusCodeException(404, "Admin account not found");
            }

            return result;
        }
    }
}