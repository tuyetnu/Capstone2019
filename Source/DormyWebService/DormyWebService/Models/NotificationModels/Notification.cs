using System;
using System.ComponentModel.DataAnnotations;
using DormyAppService.Models;
using DormyWebService.Models.AccountModels;
using DormyWebService.Models.ParamModels;

namespace DormyWebService.Models.NotificationModels
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

        [Required]
        public Param Status { get; set; }

        [Required]
        public Param Type { get; set; }
    }
}