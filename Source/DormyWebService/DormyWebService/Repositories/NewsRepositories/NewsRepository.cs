using DormyWebService.Entities;
using DormyWebService.Entities.NewsEntities;

namespace DormyWebService.Repositories.NewsRepositories
{
    public class NewsRepository : RepositoryBase<News>, INewsRepository
    {
        public NewsRepository(DormyDbContext context) : base(context)
        {
        }
    }
}