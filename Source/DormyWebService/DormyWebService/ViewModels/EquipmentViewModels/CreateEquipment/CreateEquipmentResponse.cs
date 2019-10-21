using System;
using DormyWebService.Entities.EquipmentEntities;
using DormyWebService.Entities.RoomEntities;

namespace DormyWebService.ViewModels.EquipmentViewModels.CreateEquipment
{
    public class CreateEquipmentResponse
    {
        public int EquipmentId { get; set; }
        public string Name { get; set; }
        public int? RoomId { get; set; }
        public string ImageUrl { get; set; }
        public string Status { get; set; }
        public decimal Price { get; set; }
        public string CreatedDate { get; set; }
        public string LastUpdated { get; set; }

        public static CreateEquipmentResponse CreateFromEquipment(Equipment equipment)
        {
            var result = new CreateEquipmentResponse()
            {
                CreatedDate = equipment.CreatedDate.ToString("dd/MM/yyyy HH:mm:ss"),
                LastUpdated = equipment.LastUpdated.ToString("dd/MM/yyyy HH:mm:ss"),
                EquipmentId = equipment.EquipmentId,
                ImageUrl = equipment.ImageUrl,
                Name = equipment.Name,
                Price = equipment.Price,
                Status = equipment.Status,
                RoomId = equipment.RoomId
            };

            return result;
        }
    }
}