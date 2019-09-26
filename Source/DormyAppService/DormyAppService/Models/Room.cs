using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DormyAppService.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        public ICollection<> Type { get; set; }


    }
}