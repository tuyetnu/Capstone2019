using System.ComponentModel.DataAnnotations;

namespace DormyWebService.Entities.RoomEntities
{
    public class RoomDivision
    {
        [Key]
        public int RoomDivisionId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int RoomGroupNumber { get; set; }

        [Required]
        public int RoomNumber { get; set; }

    }
}