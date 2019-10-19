using DormyWebService.Entities;
using DormyWebService.Entities.AccountEntities;

namespace DormyWebService.Repositories.UserRepositories
{
    public class StudentRepository : RepositoryBase<Student>, IStudentRepository
    {
        public StudentRepository(DormyDbContext context) : base(context)
        {
        }
    }
}