using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DormyWebService.Entities.RoomEntities;

namespace DormyWebService.ViewModels.RoomViewModels.UpdateRoom
{
    public class UpdateRoomRequest
    {
        [Required]
        public int RoomId { get; set; }

        public string Name { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

//        public List<int> EquipmentIds { get; set; }

        [Required]
        public int Capacity { get; set; }

        [Required]
        public int RoomType { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string RoomStatus { get; set; }

        public static Room UpdateToRoom(Room room, UpdateRoomRequest request)
        {
            room.Capacity = request.Capacity;
            room.LastUpdated = DateTime.Now;
            room.Description = request.Description;
            room.Price = request.Price;
            room.Name = request.Name;
            room.RoomType = request.RoomType;
            room.RoomStatus = request.RoomStatus;

            return room;
        }
    }
}