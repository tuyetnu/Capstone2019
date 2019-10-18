using System.Threading.Tasks;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.ViewModels.AccountModelViews;
using Microsoft.AspNetCore.Mvc;

namespace DormyWebService.Services.UserServices
{
    //Implemented by UserService
    public interface IUserService
    {
        Task<ActionResult<LoginSuccessUser>> Authenticate(SocialUser socialUser);
        Task<ActionResult<User>> ChangeStatus(int id,string status);
        Task<ActionResult<User>> ChangeRole(int id, string role);
    }
}