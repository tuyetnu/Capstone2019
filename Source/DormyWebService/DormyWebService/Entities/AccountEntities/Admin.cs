using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormyWebService.Entities.AccountEntities
{
    public class Admin
    {
        //One-to-One relationship with User
        [Key]
        [ForeignKey("User")]
        public int StudentId { get; set; }

        //One-to-one user
        public virtual User User { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        //CMND
        [Required]
        public string IdentityNumber { get; set; }
    }
}
