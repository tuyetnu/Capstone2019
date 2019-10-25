using System.Net;

namespace DormyWebService.Utilities
{
    public class HttpCodeReturn
    {
        public HttpStatusCode Code { get; set; }
        public string Message { get; set; }

        public HttpCodeReturn(HttpStatusCode code)
        {
            Code = code;
        }

        public HttpCodeReturn(HttpStatusCode code, string message)
        {
            Code = code;
            Message = message;
        }
    }
}