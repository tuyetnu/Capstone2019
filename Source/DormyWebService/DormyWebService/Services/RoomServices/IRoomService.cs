using System.Collections.Generic;
using System.Threading.Tasks;
using DormyWebService.Entities.RoomEntities;
using DormyWebService.ViewModels.RoomViewModels;
using DormyWebService.ViewModels.RoomViewModels.ArrangeRoom;
using DormyWebService.ViewModels.RoomViewModels.CreateRoom;
using DormyWebService.ViewModels.RoomViewModels.GetRoomTypeInfo;
using DormyWebService.ViewModels.RoomViewModels.UpdateRoom;
using Sieve.Models;

namespace DormyWebService.Services.RoomServices
{
    public interface IRoomService
    {
        Task<Room> FindById(int id);
        Task<CreateRoomResponse> CreateRoom(CreateRoomRequest requestModel);
        Task<List<Room>> AdvancedGetRooms(string sorts, string filters, int? page, int? pageSize);
        Task<UpdateRoomResponse> UpdateRoom(UpdateRoomRequest requestModel);
        Task<ArrangeRoomResponse> ArrangeRoomForAllApprovedRequests();
        Task<List<GetRoomTypeInfoResponse>> GetRoomTypeInfo();
    }
}