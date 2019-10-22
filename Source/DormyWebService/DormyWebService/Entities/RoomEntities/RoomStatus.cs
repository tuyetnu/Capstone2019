using System.Collections.Generic;

namespace DormyWebService.Entities.RoomEntities
{
    public class RoomStatus
    {
        public const string Active = "Active";
        public const string InActive = "InActive";
        private static readonly List<string> RoomStatusList = new List<string>()
        {
            Active,
            InActive
        };

        public static bool IsRoomStatus(string status)
        {
            return RoomStatusList.Contains(status);
        }

        public static string ListAllStatuses()
        {
            return string.Join(", ", RoomStatusList);
        }
    }
}