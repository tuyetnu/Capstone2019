using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Services.RoomServices;
using DormyWebService.Utilities;
using DormyWebService.ViewModels.RoomViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

            try
            {
                return await _room.CreateRoom(request);
            }
            catch (HttpStatusCodeException e)
            {
                return StatusCode(e.StatusCode, e.Message);
            }
        }
    }
}