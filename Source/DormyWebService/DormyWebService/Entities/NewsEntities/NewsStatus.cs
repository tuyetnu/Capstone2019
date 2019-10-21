using System.Collections.Generic;

namespace DormyWebService.Entities.NewsEntities
{
    public static class NewsStatus
    {
        public const string Active = "Active";
        public const string InActive = "InActive";

        public static readonly List<string> NewsStatusList = new List<string>()
        {
            Active,
            InActive
        };

        public static bool IsNewsStatus(string status)
        {
            return NewsStatusList.Contains(status);
        }
    }
}