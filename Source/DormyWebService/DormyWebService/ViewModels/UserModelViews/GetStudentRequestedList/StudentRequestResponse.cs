using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DormyWebService.ViewModels.UserModelViews.GetStudentRequestedList
{
    public class StudentRequestResponse
    {
        public string requestType { get; set; }

        public int requestId { get; set; }

        public string status { get; set; }

        public DateTime createDate { get; set; }

        public DateTime lasteUpdate { get; set; }

        public StudentRequestResponse(string requestType, int requestId, string status, DateTime createDate, DateTime lasteUpdate)
        {
            this.requestType = requestType;
            this.requestId = requestId;
            this.status = status;
            this.createDate = createDate;
            this.lasteUpdate = lasteUpdate;
        }
    }
}
