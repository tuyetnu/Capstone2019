using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormyWebService.Models.ParamModels
{
    public class ParamType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ParamTypeId { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}