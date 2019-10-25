using DormyWebService.Entities;
using DormyWebService.Entities.MoneyEntities;

namespace DormyWebService.Repositories.MoneyRepositories
{
    public class StudentMonthlyBillRepository : RepositoryBase<StudentMonthlyBill>, IStudentMonthlyBillRepository
    {
        public StudentMonthlyBillRepository(DormyDbContext context) : base(context)
        {
        }
    }
}