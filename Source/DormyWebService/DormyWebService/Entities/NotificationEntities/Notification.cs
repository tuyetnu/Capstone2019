using System;
using System.ComponentModel.DataAnnotations;
using DormyWebService.Entities.AccountEntities;

namespace DormyWebService.Entities.NotificationEntities
{
    public class Notification
    {
        [Required]
        public int NotificationId { get; set; }

        public User Owner { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        public string Url { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public DateTime LastUpdated { get; set; }

        //Param
        [Required]
        public int Status { get; set; }

        //Param
        [Required]
        public int Type { get; set; }
    }
}