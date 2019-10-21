using System;
using System.Threading.Tasks;
using AutoMapper;
using DormyWebService.Entities.RoomEntities;
using DormyWebService.Repositories;
using DormyWebService.Services.UserServices;
using DormyWebService.Utilities;

namespace DormyWebService.Services.RoomServices
{
    public class RoomService : IRoomService
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private IMapper _mapper;
        private IAdminService _admin;

        public RoomService(IRepositoryWrapper repoWrapper, IMapper mapper, IAdminService admin)
        {
            _repoWrapper = repoWrapper;
            _mapper = mapper;
            _admin = admin;
        }

        public async Task<Room> FindById(int id)
        {
            Room result;
            try
            {
                result = await _repoWrapper.Room.FindByIdAsync(id);
            }
            catch (Exception)
            {
                throw new HttpStatusCodeException(500, "Internal Server Error Occured when finding room");
            }

            //Check if there's this user in database
            if (result == null)
            {
                throw new HttpStatusCodeException(404, "Room is not found");
            }

            return result;
        }
    }
}