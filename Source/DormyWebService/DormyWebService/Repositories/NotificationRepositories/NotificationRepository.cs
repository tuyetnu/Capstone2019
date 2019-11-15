using DormyWebService.Entities;
using DormyWebService.Entities.NotificationEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DormyWebService.Repositories.NotificationRepositories
{
    public class NotificationRepository : RepositoryBase<Notification>, INotificationRepository
    {
        public NotificationRepository(DormyDbContext context) : base(context)
        {
        }
    }
}
