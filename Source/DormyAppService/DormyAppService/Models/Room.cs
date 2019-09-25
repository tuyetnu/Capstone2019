using System.ComponentModel.DataAnnotations;

namespace DormyAppService.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(20)]
        public string Name { get; set; }

    }
}