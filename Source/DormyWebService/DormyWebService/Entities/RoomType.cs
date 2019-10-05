using System.ComponentModel.DataAnnotations;

namespace DormyWebService.Entities.Room
{
    public class RoomType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int DefaultCapacity { get; set; }

    }
}
