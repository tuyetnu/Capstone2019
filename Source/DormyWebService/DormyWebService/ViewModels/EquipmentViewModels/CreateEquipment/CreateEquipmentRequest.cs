using System;
using System.ComponentModel.DataAnnotations;
using DormyWebService.Entities.EquipmentEntities;
using DormyWebService.Entities.RoomEntities;
using DormyWebService.Utilities;

namespace DormyWebService.ViewModels.EquipmentViewModels.CreateEquipment
{
    public class CreateEquipmentRequest
    {
        public int? RoomId { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int EquipmentTypeId { get; set; }

        public static Equipment NewEquipmentFromRequest(CreateEquipmentRequest request, string codePrefix)
        {
            var result = new Equipment()
            {
                Code = codePrefix,
                Status = request.Status,
                CreatedDate = DateTime.Now.AddHours(GlobalParams.TimeZone),
                LastUpdated = DateTime.Now.AddHours(GlobalParams.TimeZone),
                ImageUrl = request.ImageUrl,
                Price = request.Price,
                RoomId = request.RoomId,
                EquipmentTypeId = request.EquipmentTypeId
            };

            return result;
        }
    }
}