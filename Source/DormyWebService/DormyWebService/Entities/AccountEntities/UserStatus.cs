using System.Collections.Generic;

namespace DormyWebService.Entities.AccountEntities
{
    public static class UserStatus
    {
        public const string Active = "Active";
        public const string InActive = "InActive";
        private static readonly List<string> StatusList = new List<string>()
        {
            Active,
            InActive
        };

        public static List<string> GetAllStatus()
        {
            return StatusList;
        }

        public static bool IsRole(string status)
        {
            return StatusList.Contains(status);
        }
    }
}