using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SocialMediaCore.Exceptions;

namespace SocialMediaInfrastructure.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if(context.Exception.GetType() == typeof(BussinesException))
            {
                var exception = (BussinesException) context.Exception;
                var validation = new
                {
                    status = 400,
                    title = "Bad Request",
                    Detail = exception.Message
                };

                var json = new {
                    errors = new[] {validation}
                };

                context.Result = new BadRequestObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.ExceptionHandled = true;
                
            }
        }
    }
}