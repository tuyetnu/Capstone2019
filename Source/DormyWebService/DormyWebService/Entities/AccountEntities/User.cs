using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sieve.Attributes;

namespace DormyWebService.Entities.AccountEntities
{
    //int is for int primary key
    public class User
    {
        [Key]
        [Sieve(CanFilter = true, CanSort = true)]
        public int UserId { get; set; }

        [EmailAddress]
        [Sieve(CanFilter = true, CanSort = true)]
        public string Email { get; set; }

        //Param
        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public string Status { get; set; }

        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public string Role { get; set; }
    }
}
