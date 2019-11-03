using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormyWebService.Entities.RoomEntities
{
    public class RoomsAndEquipmentTypes
    {
        [Key]
        [ForeignKey("Room")]
        public int RoomId { get; set; }
        public Room Room { get; set; }

        [Key]
        public int RoomTypeId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required] 
        public int RealQuantity { get; set; }
    }
}