using System.Collections.Generic;

namespace DormyWebService.ViewModels.EquipmentViewModels.GetEquipment
{
    public class AdvancedGetEquipmentResponse
    {
        public List<GetEquipmentResponse> ResultList { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
    }
}