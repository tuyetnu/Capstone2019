using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DormyAppService.Models.AccountModels;

namespace DormyAppService.Models.DocumentModels
{
    public class Document
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public DocumentType Type { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        public DateTime LastUpdated { get; set; }
    }
}