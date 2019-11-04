using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.RoomEntities;
using DormyWebService.Services.RoomServices;
using DormyWebService.Utilities;
using DormyWebService.ViewModels.RoomViewModels;
using DormyWebService.ViewModels.RoomViewModels.ArrangeRoom;
using DormyWebService.ViewModels.RoomViewModels.CreateRoom;
using DormyWebService.ViewModels.RoomViewModels.GetAllMissingEquipmentRoom;
using DormyWebService.ViewModels.RoomViewModels.GetRoomTypeInfo;
using DormyWebService.ViewModels.RoomViewModels.UpdateRoom;
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.ImportRoomBooking;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace DormyWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _room;
        private readonly IRoomsAndEquipmentTypesService _roomsAndEquipmentTypes;

        public RoomsController(IRoomService room, IRoomsAndEquipmentTypesService roomsAndEquipmentTypes)
        {
            _room = room;
            _roomsAndEquipmentTypes = roomsAndEquipmentTypes;
        }

        /// <summary>
        /// Get list of room with pagination and condition, for admin and staff
        /// </summary>
        /// <param name="sorts">Ex:"Name,Description,-CreatedDate" sort by Name, then Description, then descendingly by CreatedDate</param>
        /// <param name="filters">Ex:"Price>10, Name@=roomName" filter to room with price more than 10, and a name that contains roomName</param>
        /// <param name="page">Ex: 1 get the first page...</param>
        /// <param name="pageSize">Ex: 10 ...which contains 10 room</param>
        /// <returns></returns>
        [Authorize(Roles = Role.Admin + "," + Role.Staff)]
        [HttpGet]
        public async Task<ActionResult<List<Room>>> AdvancedGetRooms(string sorts, string filters, int? page, int? pageSize)
        {
                return await _room.AdvancedGetRooms(sorts,filters,page,pageSize);
        }

        /// <summary>
        /// Get general information about a room type, for authorized users
        /// </summary>
        /// <returns></returns>
//        [Authorize]
        [HttpGet("GetRoomTypeInfo")]
        public async Task<ActionResult<List<GetRoomTypeInfoResponse>>> GetRoomTypeInfo()
        {
            return await _room.GetRoomTypeInfo();
        }

        /// <summary>
        /// Get list of missing equipment type, for admin and staff
        /// </summary>
        /// <param name="sorts"></param>
        /// <param name="filters"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.Admin + "," + Role.Staff)]
        [HttpGet("GetAllMissingEquipmentRoom")]
        public async Task<ActionResult<AdvancedGetAllMissingEquipmentRoomResponse>> GetAllMissingEquipmentRoom(
            string sorts, string filters, int? page, int? pageSize)
        {
            return await _roomsAndEquipmentTypes.GetAllMissingEquipmentRoom(sorts, filters, page, pageSize);
        }

        /// <summary>
        /// Create Building with rooms, For Admin
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.Admin)]
        [HttpPost("Building")]
        public async Task<ActionResult<BuildingResponse>> CreateBuilding(CreateBuildingRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return await _room.CreateBuilding(request);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet("Building")]
        public async Task<List<Building>> GetAllBuilding()
        {
            return await _room.GetAllBuilding();
        }

        /// <summary>
        /// Update Room information, for admin, equipment is updated separately
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult<UpdateRoomResponse>> UpdateRoom(UpdateRoomRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!RoomStatus.IsRoomStatus(request.RoomStatus))
            {
                return BadRequest("RoomsController: RoomStatus is invalid. Must be: " + RoomStatus.ListAllStatuses());
            }

            return await _room.UpdateRoom(request);
        }

        /// <summary>
        /// Import list of roomBooking and arrange room for students, for staff and admin
        /// </summary>
        /// <returns></returns>
//        [Authorize(Roles = Role.Admin + "," + Role.Staff)]
        [HttpPost("ImportRoomBooking")]
        public async Task<ActionResult<ArrangeRoomResponse>> ImportRoomBooking(List<ImportRoomBookingRequest> requests)
        {
            return await _room.ImportRoomBookingRequests(requests);
        }
    }
}