using System.Threading.Tasks;
using DormyWebService.Entities.MoneyEntities;
using DormyWebService.ViewModels.PaymentModels;

namespace DormyWebService.Repositories.MoneyRepositories
{
    public interface IStudentMonthlyBillRepository : IRepository<StudentMonthlyBill>
    {
        StudentBillResponse GetBillByRequest(StudentBillRequest studentBillRequest);
    }
}