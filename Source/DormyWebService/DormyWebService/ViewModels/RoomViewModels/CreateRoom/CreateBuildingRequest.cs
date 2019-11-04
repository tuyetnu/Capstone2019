using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DormyWebService.Entities.RoomEntities;

namespace DormyWebService.ViewModels.RoomViewModels.CreateRoom
{
    public class CreateBuildingRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int NumberOfFloor { get; set; }

        [Required]
        public int RoomOnEachFloor { get; set; }
        
        public List<CreateRoomRequest> CreateRoomRequests { get; set; }

        public static Building NewBuildingAndRoomsFromRequest(CreateBuildingRequest request)
        {
            return new Building()
            {
                Name = request.Name,
                CreatedDate = DateTime.Now,
                LastUpdated = DateTime.Now,
                NumberOfFloor = request.NumberOfFloor,
                RoomOnEachFloor = request.RoomOnEachFloor,
                Status = "Active"
            };
        }
    }
}