using System;
using DormyWebService.Entities.EquipmentEntities;
using DormyWebService.Entities.RoomEntities;
using DormyWebService.Utilities;

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
        public string CreatedDate { get; set; }
        public string LastUpdated { get; set; }

        public static UpdateEquipmentResponse CreateFromEquipment(Equipment equipment, Room room)
        {
            var result = new UpdateEquipmentResponse()
            {
                CreatedDate = equipment.CreatedDate.ToString(GlobalParams.DateTimeResponseFormat),
                LastUpdated = equipment.LastUpdated.ToString(GlobalParams.DateTimeResponseFormat),
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