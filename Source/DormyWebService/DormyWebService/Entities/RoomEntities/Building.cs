using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DormyWebService.Entities.RoomEntities
{
    public class Building
    {
        [Key]
        public int BuildingId { get; set; }

        [Required]
        public string Name { get; set; }

//        [Required]
//        public int RoomNumber { get; set; }

        [Required]
        public int NumberOfFloor { get; set; }

        [Required]
        public int RoomOnEachFloor { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime LastUpdated { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }

    }
}