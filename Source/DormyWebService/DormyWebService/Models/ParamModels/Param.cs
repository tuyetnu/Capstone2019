using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using DormyWebService.Models.EquipmentModels;
using DormyWebService.Models.MoneyModels;
using DormyWebService.Models.NotificationModels;

namespace DormyWebService.Models.ParamModels
{
    public class Param
    {
        //Id is not generated
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ParamId { get; set; }

        public ParamType ParamType { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Equipment> Equipments { get; set; }
        public virtual ICollection<MoneyTransaction> MoneyTransactions { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
