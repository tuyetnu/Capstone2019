using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormyWebService.Entities.Account
{
    public class Staff
    {
        //Foreign key is also Key, point's to id in account table
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AccountId { get; set; }
        public Account Account { get; set; }
        //Số CMND
        public string IdentityCardId { get; set; }

        //Quê Quán
        public string HomeTown { get; set; }

        public string PhoneNumber { get; set; }

        [Required]
        public bool Gender { get; set; }
    }
}