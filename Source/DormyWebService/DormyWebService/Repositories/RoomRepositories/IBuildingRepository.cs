using System.Threading.Tasks;
using DormyWebService.Entities.RoomEntities;

namespace DormyWebService.Repositories.RoomRepositories
{
    public interface IBuildingRepository : IRepository<Building>
    {
        Building GetAllIncludeRoomAndStudentById(int buildingId);
    }
}