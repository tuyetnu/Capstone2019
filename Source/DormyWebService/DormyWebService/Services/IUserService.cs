using System.Collections.Generic;
using System.Threading.Tasks;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.ViewModels.AccountModelViews;

namespace DormyWebService.Services
{
    //Implemented by UserService
    public interface IUserService
    {
        Task<LoginSuccessUser> Authenticate(SocialUser socialUser);
        Task<List<User>> FindAll();
    }
}