using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DormyWebService.ViewModels.UserModelViews.SendFCMDeviceToken
{
    public class SendFCMDeviceTokenRequest
    {
        public int UserId { get; set; }
        public string FCMDeviceToken { get; set; }
    }
}
