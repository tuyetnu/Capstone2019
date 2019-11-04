using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DormyWebService.Entities.RoomEntities;

namespace DormyWebService.ViewModels.RoomViewModels.CreateRoom
{
    public class CreateRoomRequest
    {
        public string Name { get; set; }

        [Required]
        public int RoomType { get; set; }

        [Required]
        public string RoomStatus { get; set; }

        [Required]
        public bool Gender { get; set; }

        public static Room NewRoomFromRequest(CreateRoomRequest request)
        {
            return new Room()
            {
                CreatedDate = DateTime.Now,
                LastUpdated = DateTime.Now,
                Name = request.Name,
                RoomType = request.RoomType,
                RoomStatus = request.RoomStatus,
                CurrentNumberOfStudent = 0,
                Gender = request.Gender
            };
        }
    }
}