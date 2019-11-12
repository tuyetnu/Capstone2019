using DormyWebService.Entities.AccountEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DormyWebService.ViewModels.UserModelViews.CheckToken
{
    public class CheckTokenResponse
    {
        public int UserId { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }

        public CheckTokenResponse(User user)
        {
            UserId = user.UserId;
            Role = user.Role;
            Status = user.Status;
        }
    }
}
