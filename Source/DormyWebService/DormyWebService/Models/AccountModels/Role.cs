using Microsoft.AspNetCore.Identity;

namespace DormyWebService.Models.AccountModels
{
    public class Role : IdentityRole<int>
    {
        public Role() { }

        public Role(string name)
        {
            Name = name;
        }
    }
}