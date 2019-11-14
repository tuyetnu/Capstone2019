using DormyWebService.Entities.AccountEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DormyWebService.Repositories.UserRepositories
{
    public interface IStudentRepository : IRepository<Student>
    {
        Student FindWithRoomBydId(int id);
        List<Student> GetAllStudentInDormitoryIncluding();
    }
}