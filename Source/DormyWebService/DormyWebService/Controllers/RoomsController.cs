using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.RoomEntities;
using DormyWebService.Services.RoomServices;
using DormyWebService.Utilities;
using DormyWebService.ViewModels.RoomViewModels;
using DormyWebService.ViewModels.RoomViewModels.CreateRoom;
using DormyWebService.ViewModels.RoomViewModels.UpdateRoom;
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

        public RoomsController(IRoomService room)
        {
            _room = room;
        }

        /// <summary>
        /// Get list of room with pagination and condition, Authorization is not determined yet
        /// </summary>
        /// <param name="sorts">Ex:"Name,Description,-CreatedDate" sort by Name, then Description, then descendingly by CreatedDate</param>
        /// <param name="filters">Ex:"Price>10, Name@=roomName" filter to room with price more than 10, and a name that contains roomName</param>
        /// <param name="page">Ex: 1 get the first page...</param>
        /// <param name="pageSize">Ex: 10 ...which contains 10 room</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<Room>>> AdvancedGetRooms(string sorts, string filters, int? page, int? pageSize)
        {
                return await _room.AdvancedGetRooms(sorts,filters,page,pageSize);
        }

        /// <summary>
        /// Create Room with multiple existing equipments, For Admin
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.Admin)]
        [HttpPost]
        public async Task<ActionResult<CreateRoomResponse>> CreateRoom(CreateRoomRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!RoomStatus.IsRoomStatus(request.RoomStatus))
            {
                return BadRequest("RoomsController: RoomStatus is invalid. Must be: " + RoomStatus.ListAllStatuses());
            }

            return await _room.CreateRoom(request);
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
    }
}