using DormyWebService.Entities.TicketEntities;
using DormyWebService.ViewModels.TicketViewModels.RenewContractRequestViewModels.GetRenewContract;
using System.Collections.Generic;

namespace DormyWebService.Repositories.TicketRepositories
{
    public interface IRenewContractRepository : IRepository<ContractRenewalForm>
    {
        List<GetRenewContractResponse> FindAllIncluding();
    }
}