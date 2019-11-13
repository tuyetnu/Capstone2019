using DormyWebService.Entities.TicketEntities;
using DormyWebService.ViewModels.TicketViewModels.CancelContract.GetCancelContract;
using System.Collections.Generic;

namespace DormyWebService.Repositories.TicketRepositories
{
    public interface ICancelContractRepository : IRepository<CancelContractForm>
    {
        List<CancelContractResponse> GetAllIncludingResponse();
    }
}