using System.Collections.Generic;

namespace DormyWebService.ViewModels.ContractViewModels.GetContractViewModel
{
    public class AdvancedGetContractResponse
    {
        public List<GetContractResponse> ResultList { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
    }
}