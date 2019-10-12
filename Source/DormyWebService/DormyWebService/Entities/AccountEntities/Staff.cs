using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormyWebService.Entities.AccountEntities
{
    public class Staff
    {
        //One-to-One relationship with User
        [Key]
        [ForeignKey("User")]
        public int StaffId { get; set; }

        //One-to-one with User
        public virtual User User { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        //Số CMND
        public string IdentityNumber { get; set; }

        //Quê Quán
        public string HomeTown { get; set; }

        [Required]
        public bool Gender { get; set; }
    }
}