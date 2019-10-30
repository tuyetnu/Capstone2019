using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormyWebService.Entities.AccountEntities;

namespace DormyWebService.Entities.RoomEntities
{
    public class RoomGroupsAndStaff
    {
        [ForeignKey("RoomGroup")]
        public int RoomGroupId { get; set; }
        public RoomGroup RoomGroup { get; set; }

        [ForeignKey("Staff")]
        public int StaffId { get; set; }
        public Staff Staff { get; set; }
    }
}