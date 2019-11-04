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

        public static GetAllMissingEquipmentRoomResponse ResponseFromEntity(RoomsAndEquipmentTypes roomsAndEquipmentTypesRoom, Room room, Entities.ParamEntities.Param equipmentType)
        {
            return new GetAllMissingEquipmentRoomResponse()
            {
                RoomId = room.RoomId,
                RoomName = room.Name,
                EquipmentTypeId = equipmentType.ParamId,
                EquipmentTypeName = equipmentType.Name,
                MissingQuantity = roomsAndEquipmentTypesRoom.Quantity - roomsAndEquipmentTypesRoom.RealQuantity
            };
        }
    }
}