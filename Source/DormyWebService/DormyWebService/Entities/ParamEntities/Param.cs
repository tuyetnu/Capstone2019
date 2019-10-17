using System;
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

        [Required]
        public int ParamTypeId { get; set; }

        [Required]
        [ForeignKey("ParamTypeId")]
        public ParamType ParamType { get; set; }

        public string Name { get; set; }

        public int Value { get; set; }

        public string TextValue { get; set; }

        public DateTime TimeValue { get; set; }
    }
}
