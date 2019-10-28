using System;
using DormyWebService.Entities.ContractEntities;
using DormyWebService.Utilities;

namespace DormyWebService.ViewModels.ContractViewModels.GetContractViewModel
{
    public class GetContractResponse
    {
        public int ContractId { get; set; }
        public int StudentId { get; set; }
        public string Status { get; set; }
        public string CreatedDate { get; set; }
        public string LastUpdate { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public static GetContractResponse ResponseFromEntity(Contract contract)
        {
            return new GetContractResponse()
            {
                Status = contract.Status,
                StudentId = contract.StudentId,
                CreatedDate = contract.CreatedDate.ToString(GlobalParams.DateTimeResponseFormat),
                LastUpdate = contract.LastUpdate.ToString(GlobalParams.DateTimeResponseFormat),
                StartDate = contract.StartDate.ToString(GlobalParams.DateTimeResponseFormat),
                EndDate = contract.EndDate.ToString(GlobalParams.DateTimeResponseFormat),
            };
        }
    }
}