using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormyWebService.Entities.AccountEntities
{
    //int is for int primary key
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        //Param
        [Required]
        public string Status { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
