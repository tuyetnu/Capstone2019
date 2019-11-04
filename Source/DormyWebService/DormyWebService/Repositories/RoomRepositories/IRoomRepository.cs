using System.Collections.Generic;
using System.Threading.Tasks;
using DormyWebService.Entities.RoomEntities;

namespace DormyWebService.Repositories.RoomRepositories
{
    public interface IRoomRepository : IRepository<Room>
    {
        Task<List<Room>> GetAllActiveRoomSortedByVacancy();
    }
}