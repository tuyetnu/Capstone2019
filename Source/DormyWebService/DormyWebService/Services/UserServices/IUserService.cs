using System.Collections.Generic;
using System.Threading.Tasks;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.ViewModels.Debug.ChangeUserRole;
using DormyWebService.ViewModels.UserModelViews;
using DormyWebService.ViewModels.UserModelViews.Login;
using Microsoft.AspNetCore.Mvc;

namespace DormyWebService.Services.UserServices
{
    //Implemented by UserService
    public interface IUserService
    {
        Task<User> FindById(int id);
        Task<List<User>> DebugFindAll();
        Task<LoginSuccessUser> Authenticate(string idToken, string email);
        Task<User> ChangeStatus(int id,string status);
        Task<DebugChangeUserRoleResponse> ChangeUserRole(int id, string role);
    }
}