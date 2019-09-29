using System.ComponentModel.DataAnnotations;

namespace DormyWebService.Entities
{
    public class Student : Account
    {
        //Số CMND
        public string IdentityCardId { get; set; }

        //Quê Quán
        public string HomeTown { get; set; }

        public string PhoneNumber { get; set; }

        [Required]
        public bool Gender { get; set; }

    }
}