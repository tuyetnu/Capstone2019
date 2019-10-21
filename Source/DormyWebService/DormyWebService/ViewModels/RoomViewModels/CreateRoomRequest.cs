﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DormyWebService.Entities.RoomEntities;

namespace DormyWebService.ViewModels.RoomViewModels
{
    public class CreateRoomRequest
    {
        public string Name { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        public List<int> EquipmentIds { get; set; }

        [Required]
        public int Capacity { get; set; }

        [Required]
        public int RoomType { get; set; }

        [Required]
        public decimal Price { get; set; }

        public static Room NewRoomFromRequest(CreateRoomRequest request)
        {
            return new Room()
            {
                Capacity = request.Capacity,
                CreatedDate = DateTime.Now,
                LastUpdated = DateTime.Now,
                Description = request.Description,
                Price = request.Price,
                Name = request.Name,
                RoomType = request.RoomType,
            };
        }
    }
}