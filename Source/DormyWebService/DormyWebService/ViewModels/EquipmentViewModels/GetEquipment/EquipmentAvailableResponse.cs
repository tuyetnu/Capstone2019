using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DormyWebService.ViewModels.EquipmentViewModels.GetEquipment
{
    public class EquipmentAvailableResponse
    {
        public int id { get; set; }
        public string equipmentName { get; set; }
        public int quantity { get; set; }
    }
}
