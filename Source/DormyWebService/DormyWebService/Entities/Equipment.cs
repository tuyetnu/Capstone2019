using System.ComponentModel.DataAnnotations;

namespace DormyWebService.Entities
{
    public class Equipment
    {
        [Key]
        public int Id { get; set; }

        [Required] 
        public EquipmentType EquipmentType { get; set; }

        [Required]
        public Room Room { get; set; }
    }
}