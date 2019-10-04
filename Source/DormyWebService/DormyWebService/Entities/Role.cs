﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormyWebService.Entities
{
    public class Role
    {
        //Not Identity
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}