using System.Collections.Generic;

namespace DormyWebService.Entities.EquipmentEntities
{
    public static class EquipmentStatus
    {
        public const string Active = "Active";
        public const string InActive = "InActive";
        public static readonly List<string> EquipmentStatusList = new List<string>()
        {
            Active,
            InActive
        };

        public static bool IsEquipmentStatus(string status)
        {
            return EquipmentStatusList.Contains(status);
        }
    }
}