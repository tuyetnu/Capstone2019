using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormyWebService.Entities.AccountEntities;
using Sieve.Attributes;

namespace DormyWebService.Entities.NewsEntities
{
    public class News
    {
        [Key]
        [Sieve(CanFilter = true, CanSort = true)]
        public int NewsId { get; set; }

        [ForeignKey("Author")]
        [Sieve(CanFilter = true, CanSort = true)]
        public int AuthorId { get; set; }
        
        public Admin Author { get; set; }

        [Required]
        [MaxLength(100)]
        [Sieve(CanFilter = true, CanSort = true)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public string AttachedFileUrl { get; set; }

        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public DateTime CreatedDate { get; set; }

        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public DateTime LastUpdate { get; set; }

        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public string Status { get; set; }
    }
}