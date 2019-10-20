using System;
using System.ComponentModel.DataAnnotations;
using DormyWebService.Entities.AccountEntities;

namespace DormyWebService.Entities.NewsEntities
{
    public class News
    {
        [Key]
        public int NewsId { get; set; }

        [Required]
        public Admin Author { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public string AttachedFileUrl { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime LastUpdate { get; set; }

        [Required]
        public string Status { get; set; }
    }
}