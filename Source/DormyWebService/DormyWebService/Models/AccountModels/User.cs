using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormyWebService.Models.AccountModels
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string AccessToken { get; set; }

    }
}
