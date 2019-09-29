using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DormyWebService.Entities
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }

        public int Capacity { get; set; }
        public RoomType RoomType { get; set; }
        public Student RoomLeader { get; set; }
        public ICollection<Student> Occupants { get; set; }
        public ICollection<Equipment> Equipments { get; set; }

    }
}