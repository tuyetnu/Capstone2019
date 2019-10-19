using System.Threading.Tasks;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.ViewModels.UserModelViews;
using Microsoft.AspNetCore.Mvc;

namespace DormyWebService.Services.UserServices
{
    //Implemented by UserService
    public interface IUserService
    {
        Task<LoginSuccessUser> Authenticate(string idToken, string email);
        Task<User> ChangeStatus(int id,string status);
        Task<User> ChangeRole(int id, string role);
    }
}