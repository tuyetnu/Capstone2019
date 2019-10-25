using System.Collections.Generic;

namespace DormyWebService.Entities.TicketEntities
{
    public static class IssueStatus
    {
        public const string Pending = "Pending";
        public const string Approved = "Approved";
        public const string Rejected = "Rejected";

        private static readonly List<string> IssueStatusList = new List<string>()
        {
            Pending,
            Approved,
            Rejected
        };

        public static bool IsIssueStatus(string status)
        {
            return IssueStatusList.Contains(status);
        }

        public static string ListAllStatuses()
        {
            return string.Join(", ", IssueStatusList);
        }
    }
}