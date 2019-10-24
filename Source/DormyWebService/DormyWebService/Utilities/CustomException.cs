using System;

namespace DormyWebService.Utilities
{
    /// <summary>  
    /// This class will allow to generate the custom exception message.  
    /// </summary>  
    public class CustomException : Exception
    {
        public CustomException()
        {
        }

        public CustomException(string message) : base(message)
        {
        }

        public CustomException(string message, string responseModel) : base(message)
        {
        }

        public CustomException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}