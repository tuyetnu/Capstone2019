using System.ComponentModel.DataAnnotations;

namespace DormyWebService.Models
{
    public class Account
    {
        public int Id { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Name { get; set; }
        
    }
}