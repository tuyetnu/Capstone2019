using System.Linq;
using System.Threading.Tasks;
using DormyWebService.Entities;
using DormyWebService.Entities.MoneyEntities;
using DormyWebService.ViewModels.PaymentModels;
using Microsoft.EntityFrameworkCore;

namespace DormyWebService.Repositories.MoneyRepositories
{
    public class StudentMonthlyBillRepository : RepositoryBase<StudentMonthlyBill>, IStudentMonthlyBillRepository
    {
        public StudentMonthlyBillRepository(DormyDbContext context) : base(context)
        {
        }

        public StudentBillResponse GetBillByRequest(StudentBillRequest request)
        {
            var find = Context.StudentMonthlyBills
                .Include(s => s.RoomMonthlyBill)
                .FirstOrDefault(b => b.TargetMonth == request.TargetMonth && b.TargetYear == request.TargetYear && b.StudentId == request.StudentId);
            return new StudentBillResponse(find);  
        }
    }
}