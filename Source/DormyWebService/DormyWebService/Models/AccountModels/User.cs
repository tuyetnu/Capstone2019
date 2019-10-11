using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DormyWebService.Models.AccountModels
{
    //int is for int primary key
    public class User : IdentityUser<int>
    {
        public virtual Student Student { get; set; }
        public virtual Staff Staff { get; set; }
    }
}
