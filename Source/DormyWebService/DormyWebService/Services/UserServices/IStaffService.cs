using System.Threading.Tasks;
using DormyWebService.Entities.AccountEntities;

namespace DormyWebService.Services.UserServices
{
    public interface IStaffService
    {
        Task<Staff> FindById(int id);
    }
}