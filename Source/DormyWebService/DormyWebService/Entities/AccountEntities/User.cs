using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormyWebService.Entities.AccountEntities
{
    //int is for int primary key
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }
        //Param
        [Required]
        public int Status { get; set; }
        public virtual Role Role { get; set; }
        public string AccessToken { get; set; }
    }
}
