using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DormyWebService.Entities.Account;

namespace DormyWebService.Entities.Room
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdated { get; set; }

        public int Capacity { get; set; }
        public RoomType RoomType { get; set; }
        public Student RoomLeader { get; set; }
        public ICollection<Student> Students { get; set; }
        public ICollection<Equipment> Equipments { get; set; }

    }
}