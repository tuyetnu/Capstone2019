using DormyWebService.Utilities;
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

        public string createDate { get; set; }

        public string lasteUpdate { get; set; }

        public StudentRequestResponse(StudentRequestResponseTemp temp)
        {
            this.requestType = temp.requestType;
            this.requestId = temp.requestId;
            this.status = temp.status;
            this.createDate = temp.createDate.ToString(GlobalParams.DateTimeResponseFormat);
            this.lasteUpdate = temp.createDate.ToString(GlobalParams.DateTimeResponseFormat);
        }
    }
}
