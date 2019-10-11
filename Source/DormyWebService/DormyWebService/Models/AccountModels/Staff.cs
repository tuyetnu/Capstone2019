using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormyWebService.Models.AccountModels
{
    [Table("Staffs")]
    public class Staff
    {
        [Key]
        [ForeignKey("User")]
        public int Id { get; set; }

        public virtual User User { get; set; }

        //Số CMND
        public string IdentityNumber { get; set; }

        //Quê Quán
        public string HomeTown { get; set; }

        [Required]
        public bool Gender { get; set; }
    }
}