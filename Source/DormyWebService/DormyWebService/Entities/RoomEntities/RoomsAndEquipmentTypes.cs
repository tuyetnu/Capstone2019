using DormyWebService.Entities.ParamEntities;
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

        [ForeignKey("Param")]
        public int EquipmentTypeId { get; set; }
        public Param Param { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required] 
        public int RealQuantity { get; set; }
    }
}