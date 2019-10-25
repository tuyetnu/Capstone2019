using System.Threading.Tasks;
using DormyWebService.ViewModels.HomeModelView;

namespace DormyWebService.Services.NewFolder
{
    public interface IHomeService
    {
        Task<HomeResponse> GetInitialValues(int studentId);
    }
}