using System.ComponentModel.DataAnnotations;

namespace DormyWebService.Entities.Account
{
    public class Account
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        //Tên thật
        [Required]
        [StringLength(3-255)]
        public string Name { get; set; }

        [Required]
        public bool Gender { get; set; }

        //Số CMND
        public string IdentityCardNumber { get; set; }

        //Ảnh CMND
        public string IdPictureUrl { get; set; }

        //SĐT
        public string PhoneNumber { get; set; }

        //Địa chỉ
        public string Address { get; set; }

        [Required]
        public Role Role { get; set; }
    }
}