using System;
using System.ComponentModel.DataAnnotations;
using DormyWebService.Entities.EquipmentEntities;
using DormyWebService.Entities.RoomEntities;

namespace DormyWebService.ViewModels.EquipmentViewModels.CreateEquipment
{
    public class CreateEquipmentRequest
    {
        public string Name { get; set; }
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Room Id must be a number")]
        public string RoomId { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public decimal Price { get; set; }

        public static Equipment NewEquipmentFromRequest(CreateEquipmentRequest request, Room room)
        {
            var result = new Equipment()
            {
                Name = request.Name,
                Status = request.Status,
                CreatedDate = DateTime.Now,
                LastUpdated = DateTime.Now,
                ImageUrl = request.ImageUrl,
                Price = request.Price,
            };

            if (room != null)
            {
                result.Room = room;
            }

            return result;
        }
    }
}