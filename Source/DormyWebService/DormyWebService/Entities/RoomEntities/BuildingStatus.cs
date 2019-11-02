using System.Collections.Generic;

namespace DormyWebService.Entities.RoomEntities
{
    public class BuildingStatus
    {
        public const string Active = "Active";
        public const string InActive = "InActive";
        private static readonly List<string> BuildingStatusList = new List<string>()
        {
            Active,
            InActive
        };

        public static bool IsBuildingStatus(string status)
        {
            return BuildingStatusList.Contains(status);
        }

        public static string ListAllStatuses()
        {
            return string.Join(", ", BuildingStatusList);
        }
    }
}