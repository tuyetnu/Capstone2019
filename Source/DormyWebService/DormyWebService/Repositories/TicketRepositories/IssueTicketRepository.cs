using DormyWebService.Entities;
using DormyWebService.Entities.TicketEntities;

namespace DormyWebService.Repositories.TicketRepositories
{
    public class IssueTicketRepository : RepositoryBase<IssueTicket>, IIssueTicketRepository
    {
        public IssueTicketRepository(DormyDbContext context) : base(context)
        {
        }
    }
}