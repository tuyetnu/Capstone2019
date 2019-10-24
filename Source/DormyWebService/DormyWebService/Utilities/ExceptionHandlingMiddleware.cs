using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace DormyWebService.Utilities
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate m_next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            m_next = next;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await m_next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var httpStatusCode = 500;
            var message = "Internal Server Exception!";

            if (exception is HttpStatusCodeException codeException)
            {
                httpStatusCode = codeException.StatusCode; // Or whatever status code you want to return
                message = codeException.Message; // Or whatever message you want to return
            }

            var result = JsonConvert.SerializeObject(new
            {
                error = message
            });

            context.Response.StatusCode = (int)httpStatusCode;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(result);
        }
    }
}