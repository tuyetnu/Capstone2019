using AutoMapper;
using DormyWebService.Repositories;
using Sieve.Services;

namespace DormyWebService.Services.TicketServices
{
    public class IssueTicketService : IIssueTicketService
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private IMapper _mapper;
        private ISieveProcessor _sieveProcessor;

        public IssueTicketService(IRepositoryWrapper repoWrapper, IMapper mapper, ISieveProcessor sieveProcessor)
        {
            _repoWrapper = repoWrapper;
            _mapper = mapper;
            _sieveProcessor = sieveProcessor;
        }
    }
}