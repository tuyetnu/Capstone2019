using System.Collections.Generic;
using System.Threading.Tasks;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.ViewModels.UserModelViews;

namespace DormyWebService.Services.UserServices
{
    public interface IStudentService
    {
        Task<ICollection<Student>> GetAllStudent();
        Task<Student> FindById(int id);
        Task<Student> UpdateSelf(UpdateStudentForm requestModel);
    }
}