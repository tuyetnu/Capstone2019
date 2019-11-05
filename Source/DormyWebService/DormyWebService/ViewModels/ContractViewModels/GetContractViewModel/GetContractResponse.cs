using System;
using DormyWebService.Entities.ContractEntities;
using DormyWebService.Utilities;
using Sieve.Attributes;

namespace DormyWebService.ViewModels.ContractViewModels.GetContractViewModel
{
    public class GetContractResponse
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public int ContractId { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public int StudentId { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string Status { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string CreatedDate { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string LastUpdate { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string StartDate { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
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