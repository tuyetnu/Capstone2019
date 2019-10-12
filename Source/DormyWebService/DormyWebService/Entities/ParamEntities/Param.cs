using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormyWebService.Entities.ParamEntities
{
    public class Param
    {
        //Id is not generated
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ParamId { get; set; }

        public ParamType ParamType { get; set; }

        public string Name { get; set; }
    }
}
