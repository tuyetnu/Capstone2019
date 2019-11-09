using System;
using System.Linq;
using DormyWebService.Entities;
using DormyWebService.Entities.RoomEntities;
using Microsoft.EntityFrameworkCore;

namespace DormyWebService.Repositories.RoomRepositories
{
    public class BuildingRepository : RepositoryBase<Building>, IBuildingRepository
    {
        public BuildingRepository(DormyDbContext context) : base(context)
        {
        }

        public Building GetAllIncludeRoomAndStudentById(int buildingId)
        {
            var result = Context.Buildings
                .Where(b => b.BuildingId == buildingId)
                .Include(r => r.Rooms)
                .Single(x => x.BuildingId == buildingId);
            return result;
        }
    }
}