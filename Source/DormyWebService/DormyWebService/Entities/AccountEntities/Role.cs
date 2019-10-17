using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormyWebService.Entities.AccountEntities
{
    public static class Role
    {
        public const string Admin = "Admin";
        public const string Staff = "Staff";
        public const string Student = "Student";
        public const string User = "User";

        public static List<string> GetAllRole()
        {
            return new List<string>()
            {
                Admin,
                Staff,
                Student, 
                User
            };
        }
    }
}