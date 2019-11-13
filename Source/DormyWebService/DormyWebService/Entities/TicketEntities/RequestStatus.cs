using System.Collections.Generic;

namespace DormyWebService.Entities.TicketEntities
{
    public static class RequestStatus
    {
        public const string Pending = "Pending";
        public const string Complete = "Complete";
        public const string Approved = "Approved";
        public const string Rejected = "Rejected";
        public const string Canceled = "Canceled";

        private static readonly List<string> RequestStatusList = new List<string>()
        {
            Pending,
            Complete,
            Approved,
            Rejected,
            Canceled
        };

        public static bool IsRequestStatus(string status)
        {
            return RequestStatusList.Contains(status);
        }

        public static string ListAllStatuses()
        {
            return string.Join(", ", RequestStatusList);
        }
    }
}