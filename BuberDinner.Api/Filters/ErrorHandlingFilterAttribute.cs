using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BuberDinner.Api.Filters
{
    public class ErrorHandlingFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            var problemDetails = new ProblemDetails
            {
                Type= "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                Title = "an error occured while proccssing the request",
                Status = (int)HttpStatusCode.InternalServerError,

            };
            context.Result = new ObjectResult(problemDetails);
            context.ExceptionHandled= true;
        }
    }
}
