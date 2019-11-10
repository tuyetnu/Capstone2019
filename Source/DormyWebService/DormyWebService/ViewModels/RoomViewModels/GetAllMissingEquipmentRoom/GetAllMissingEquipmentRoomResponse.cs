using DormyWebService.Entities.RoomEntities;
using Sieve.Attributes;

namespace DormyWebService.ViewModels.RoomViewModels.GetAllMissingEquipmentRoom
{
    public class GetAllMissingEquipmentRoomResponse
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public int RoomId { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string RoomName { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public int EquipmentTypeId { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string EquipmentTypeName { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public int MissingQuantity { get; set; }

        public static GetAllMissingEquipmentRoomResponse ResponseFromEntity(RoomsAndEquipmentTypes roomsAndEquipmentTypesRoom)
        {
            return new GetAllMissingEquipmentRoomResponse()
            {
                RoomId = roomsAndEquipmentTypesRoom.RoomId,
                RoomName = roomsAndEquipmentTypesRoom.Room.Name,
                EquipmentTypeId = roomsAndEquipmentTypesRoom.EquipmentTypeId,
                EquipmentTypeName = roomsAndEquipmentTypesRoom.Param.Name,
                MissingQuantity = roomsAndEquipmentTypesRoom.Quantity - roomsAndEquipmentTypesRoom.RealQuantity
            };
        }
    }
}