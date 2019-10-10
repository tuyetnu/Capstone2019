using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormyWebService.Models.AccountModels
{
    [Table("Staffs")]
    public class Staff : User
    {
        //Số CMND
        public string IdentityCardId { get; set; }

        //Quê Quán
        public string HomeTown { get; set; }

        [Required]
        public bool Gender { get; set; }
    }
}