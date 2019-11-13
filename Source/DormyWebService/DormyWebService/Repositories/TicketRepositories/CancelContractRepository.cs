using System.Collections.Generic;
using System.Linq;
using DormyWebService.Entities;
using DormyWebService.Entities.TicketEntities;
using DormyWebService.ViewModels.TicketViewModels.CancelContract.GetCancelContract;
using Microsoft.EntityFrameworkCore;

namespace DormyWebService.Repositories.TicketRepositories
{
    public class CancelContractRepository : RepositoryBase<CancelContractForm>, ICancelContractRepository
    {
        public CancelContractRepository(DormyDbContext context) : base(context)
        {
        }

        public List<CancelContractResponse> GetAllIncludingResponse()
        {
            return Context.CancelContractForms
                .Include(c => c.Contract)
                .Include(st => st.Staff)
                .Include(s => s.Student)
                    .ThenInclude(r => r.Room)
                .Select(c => CancelContractResponse.ResponseFromEntity(c))
                .ToList();
        }
    }
}