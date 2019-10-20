using System.Threading.Tasks;
using DormyWebService.Entities.AccountEntities;

namespace DormyWebService.Services.UserServices
{
    public interface IAdminService
    {
        Task<Admin> FindById(int id);
    }
}