using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DormyWebService.Entities.RoomEntities
{
    public class RoomTypes_EquipmentTypes
    {
        [Key]
        public int RoomTypeId { get; set; }
        [Key]
        public int EquipmentTypeId { get; set; }
        public int Amount { get; set; }
    }
}