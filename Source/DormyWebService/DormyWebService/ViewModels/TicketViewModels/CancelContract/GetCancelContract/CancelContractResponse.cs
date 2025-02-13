﻿using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.ContractEntities;
using DormyWebService.Entities.RoomEntities;
using DormyWebService.Entities.TicketEntities;
using DormyWebService.Utilities;
using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DormyWebService.ViewModels.TicketViewModels.CancelContract.GetCancelContract
{
    public class CancelContractResponse
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public int CancelContractFormId { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public int StudentId { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string StudentName { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string StudentEmail { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public int? StaffId { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string StaffName { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public int ContractId { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string Status { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string CreatedDate { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string LastUpdated { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public DateTime LastUpdatedDate { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string CancelationDate { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public int RoomId { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string RoomName { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public int RoomType { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string StudentCode { get; set; }

        public string Description { get; set; }

        public static CancelContractResponse ResponseFromEntity(CancelContractForm form)
        {
            return new CancelContractResponse()
            {
                StudentId = form.StudentId,
                Status = form.Status,
                CreatedDate = form.CreatedDate.ToString(GlobalParams.DateTimeResponseFormat),
                LastUpdated = form.LastUpdated.ToString(GlobalParams.DateTimeResponseFormat),
                LastUpdatedDate = form.LastUpdated,
                CancelationDate = form.CancelationDate.ToString(GlobalParams.DateTimeResponseFormat),
                StaffId = form.Staff?.StaffId ?? -1,
                StaffName = form.Staff?.Name ?? "null",
                StudentName = form.Student.Name,
                StudentCode = form.Student.StudentCardNumber,
                ContractId = form.ContractId,
                CancelContractFormId = form.CancelContractFormId,
                StudentEmail = form.Student.Email,
                RoomId = form.Student.Room.RoomId,
                RoomName = form.Student.Room.Name,
                RoomType = form.Student.Room.RoomType,
                Description = form.Description
            };
        }
    }
}
