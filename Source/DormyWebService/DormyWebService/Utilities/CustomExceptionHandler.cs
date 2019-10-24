using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DormyWebService.Utilities
{
    /// <summary>  
    /// This class is used for handle the custom exception in the application level.  
    /// </summary>  
    public class CustomExceptionHandler : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            throw new NotImplementedException();
        }
    }
}