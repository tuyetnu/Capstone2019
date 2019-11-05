using System.Collections.Generic;
using System.Threading.Tasks;
using DormyWebService.Entities.RoomEntities;

namespace DormyWebService.Repositories.RoomRepositories
{
    public interface IRoomRepository : IRepository<Room>
    {
        Task<List<Room>> GetAllActiveRoomSortedByVacancy();
        Task<List<Room>> GetAllActiveRoomWithSpecificGenderSortedByVacancy(bool gender);
        Task<List<Room>> GetAllActiveRoomWithSpecificGenderAndRoomTypeSortedByVacancy(bool gender, int roomType);
    }
}