﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DormyWebService.ViewModels.TicketViewModels.RenewContractRequestViewModels.ApproveRenewContract
{
    public class ApproveRenewContractResponse
    {
        public int RenewContractFormId;

        public ApproveRenewContractResponse(int renewContractFormId)
        {
            RenewContractFormId = renewContractFormId;
        }
    }
}
