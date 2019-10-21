using System;
using DormyWebService.Entities.EquipmentEntities;
using DormyWebService.Entities.RoomEntities;

namespace DormyWebService.ViewModels.EquipmentViewModels.UpdateEquipment
{
    public class UpdateEquipmentResponse
    {
        public int EquipmentId { get; set; }
        public string Name { get; set; }
        public string RoomId { get; set; }
        public string ImageUrl { get; set; }
        public string Status { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdated { get; set; }

        public static UpdateEquipmentResponse CreateFromEquipment(Equipment equipment, Room room)
        {
            var result = new UpdateEquipmentResponse()
            {
                CreatedDate = equipment.CreatedDate,
                LastUpdated = equipment.LastUpdated,
                EquipmentId = equipment.EquipmentId,
                ImageUrl = equipment.ImageUrl,
                Name = equipment.Name,
                Price = equipment.Price,
                Status = equipment.Status,
            };

            if (room != null)
            {
                result.RoomId = room.RoomId.ToString();
            }

            return result;
        }
    }
}