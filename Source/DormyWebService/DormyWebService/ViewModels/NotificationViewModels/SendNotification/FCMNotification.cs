using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DormyWebService.ViewModels.NotificationViewModels.SendNotification
{
    public class FCMNotification
    {
        public string[] registration_ids { get; set; }
        public Notification notification { get; set; }
    }
    public class Notification
    {
        public string title { get; set; }
        public string text { get; set; }
    }
}
