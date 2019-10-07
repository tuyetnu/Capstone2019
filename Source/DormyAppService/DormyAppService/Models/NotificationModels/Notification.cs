using System;
using System.ComponentModel.DataAnnotations;
using DormyAppService.Models.AccountModels;

namespace DormyAppService.Models.NotificationModels
{
    public class Notification
    {
        [Required]
        public int Id { get; set; }

        public Student Student { get; set; }

        [MaxLength(200)]
        public string Content { get; set; }

        public string Url { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public DateTime LastUpdated { get; set; }

        [Required]
        public NotificationStatus Status { get; set; }

        [Required]
        public NotificationType Type { get; set; }
    }
}