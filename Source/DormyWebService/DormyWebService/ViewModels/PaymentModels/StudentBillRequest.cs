using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DormyWebService.ViewModels.PaymentModels
{
    public class StudentBillRequest
    {
        public int StudentId { get; set; }
        public int TargetMonth { get; set; }
        public int TargetYear { get; set; }
    }
}
