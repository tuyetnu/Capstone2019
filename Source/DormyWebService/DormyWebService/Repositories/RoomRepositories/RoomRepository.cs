using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DormyWebService.Entities;
using DormyWebService.Entities.RoomEntities;
using Microsoft.EntityFrameworkCore;
using Remotion.Linq.Clauses;

namespace DormyWebService.Repositories.RoomRepositories
{
    public class RoomRepository : RepositoryBase<Room>, IRoomRepository
    {
        public RoomRepository(DormyDbContext context) : base(context)
        {
        }

        public async Task<List<Room>> GetAllActiveRoomSortedByVacancy()
        {
            var rooms = from room in Context.Rooms where room.RoomStatus == RoomStatus.Active && (room.Capacity - room.CurrentNumberOfStudent) > 0 select room;

            rooms = rooms.OrderBy(r => (r.Capacity - r.CurrentNumberOfStudent));

            return await rooms.AsNoTracking().ToListAsync();
        }

        public async Task<List<Room>> GetAllActiveRoomWithSpecificGenderSortedByVacancy(bool gender)
        {
            var rooms = from room in Context.Rooms where room.RoomStatus == RoomStatus.Active && (room.Capacity - room.CurrentNumberOfStudent) > 0 && room.Gender == gender select room;

            rooms = rooms.OrderBy(r => (r.Capacity - r.CurrentNumberOfStudent));

            return await rooms.AsNoTracking().ToListAsync();
        }

        public async Task<List<Room>> GetAllActiveRoomWithSpecificGenderAndRoomTypeSortedByVacancy(bool gender, int roomType)
        {
            var rooms = from room in Context.Rooms where room.RoomStatus == RoomStatus.Active && (room.Capacity - room.CurrentNumberOfStudent) > 0 && room.Gender == gender && room.RoomType == roomType select room;

            rooms = rooms.OrderBy(r => (r.Capacity - r.CurrentNumberOfStudent));

            return await rooms.AsNoTracking().ToListAsync();
        }
    }
}