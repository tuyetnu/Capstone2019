using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormyWebService.Entities.EquipmentEntities;
using DormyWebService.Entities.RoomEntities;
using DormyWebService.Utilities;
using Sieve.Attributes;

namespace DormyWebService.ViewModels.EquipmentViewModels.GetEquipment
{
    public class GetEquipmentResponse
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public int EquipmentId { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string Code { get; set; }

        //Equipment
        [Sieve(CanFilter = true, CanSort = true)]
        public int EquipmentTypeId { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string EquipmentTypeName { get; set; }

        //Room
        [Sieve(CanFilter = true, CanSort = true)]
        public int? RoomId { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string RoomName { get; set; }

//        public string ImageUrl { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string Status { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public decimal Price { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string CreatedDate { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string LastUpdated { get; set; }

        public static GetEquipmentResponse ResponseFromEntity(Equipment equipment, Entities.ParamEntities.Param equipmentType, Room room)
        {
            return new GetEquipmentResponse()
            {
                Status = equipment.Status,
                RoomId = room?.RoomId ?? -1,
                RoomName = room?.Name ?? "null",
                CreatedDate = equipment.CreatedDate.ToString(GlobalParams.DateTimeResponseFormat),
                LastUpdated = equipment.LastUpdated.ToString(GlobalParams.DateTimeResponseFormat),
                EquipmentId = equipment.EquipmentId,
                Price = equipment.Price,
                Code = equipment.Code,
                EquipmentTypeId = equipmentType.ParamTypeId,
                EquipmentTypeName = equipmentType.Name,
                
            };
        }
    }
}