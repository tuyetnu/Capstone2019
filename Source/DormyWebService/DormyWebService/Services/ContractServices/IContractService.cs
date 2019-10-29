using System.Collections.Generic;
using System.Threading.Tasks;
using DormyWebService.ViewModels.ContractViewModels.GetContractViewModel;

namespace DormyWebService.Services.ContractServices
{
    public interface IContractService
    {
        Task<List<GetContractResponse>> AdvancedGetContracts(string sorts, string filters, int? page, int? pageSize);

        Task<List<GetContractResponse>> GetByStudentId(int id);
    }
}