using System.Collections.Generic;
using System.Threading.Tasks;
using DormyWebService.Entities.RoomEntities;
using DormyWebService.ViewModels.RoomViewModels;
using DormyWebService.ViewModels.RoomViewModels.ArrangeRoom;
using DormyWebService.ViewModels.RoomViewModels.CreateRoom;
using DormyWebService.ViewModels.RoomViewModels.GetRoomTypeInfo;
using DormyWebService.ViewModels.RoomViewModels.UpdateRoom;
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.ImportRoomBooking;
using Sieve.Models;

namespace DormyWebService.Services.RoomServices
{
    public interface IRoomService
    {
        Task<Room> FindById(int id);
        Task<List<Room>> ParseRoomAsync(List<CreateRoomRequest> createRoomRequests);
        Task<BuildingResponse> CreateBuilding(CreateBuildingRequest requestModel);
        Task<List<Room>> AdvancedGetRooms(string sorts, string filters, int? page, int? pageSize);
        Task<UpdateRoomResponse> UpdateRoom(UpdateRoomRequest requestModel);
        
        Task<List<GetRoomTypeInfoResponse>> GetRoomTypeInfo();
        Task<List<Building>> GetAllBuilding();
        Task<List<RoomsAndEquipmentTypes>> GetAllMissingEquipmentRoom();
        Task<Building> GetBuildingById(int buildingId);
    }
}