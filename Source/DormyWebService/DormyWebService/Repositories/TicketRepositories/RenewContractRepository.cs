using System.Collections.Generic;
using System.Linq;
using DormyWebService.Entities;
using DormyWebService.Entities.TicketEntities;
using DormyWebService.ViewModels.TicketViewModels.RenewContractRequestViewModels.GetRenewContract;
using Microsoft.EntityFrameworkCore;

namespace DormyWebService.Repositories.TicketRepositories
{
    public class RenewContractRepository : RepositoryBase<ContractRenewalForm>, IRenewContractRepository
    {
        public RenewContractRepository(DormyDbContext context) : base(context)
        {
        }

        public List<GetRenewContractResponse> FindAllIncluding()
        {
            return Context.ContractRenewalForms
            .Include(c =>c.Contract)
            .Include(c =>c.Contract)
            .Include(st => st.Staff)
            .Include(s => s.Student)
                .ThenInclude(r => r.Room)
            .Select(c => GetRenewContractResponse.ResponseFromEntity(c))
            .ToList();
        }
    }
}