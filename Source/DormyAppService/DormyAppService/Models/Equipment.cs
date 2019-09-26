using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DormyAppService.Models
{
    public class Equipment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime InstallTime { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

        [Required]
        public Room Room { get; set; }
    }
}