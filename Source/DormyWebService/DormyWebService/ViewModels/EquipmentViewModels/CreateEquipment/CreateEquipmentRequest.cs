using System;
using System.ComponentModel.DataAnnotations;
using DormyWebService.Entities.EquipmentEntities;
using DormyWebService.Entities.RoomEntities;

namespace DormyWebService.ViewModels.EquipmentViewModels.CreateEquipment
{
    public class CreateEquipmentRequest
    {
        public string Code { get; set; }
        public int? RoomId { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public decimal Price { get; set; }

        public static Equipment NewEquipmentFromRequest(CreateEquipmentRequest request)
        {
            var result = new Equipment()
            {
                Code = request.Code,
                Status = request.Status,
                CreatedDate = DateTime.Now.AddHours(7),
                LastUpdated = DateTime.Now.AddHours(7),
                ImageUrl = request.ImageUrl,
                Price = request.Price,
                RoomId = request.RoomId
            };

            return result;
        }
    }
}