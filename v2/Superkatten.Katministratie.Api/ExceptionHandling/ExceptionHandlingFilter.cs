using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Superkatten.Katministratie.HttpApi.ExceptionHandling;

public class ExceptionHandlingFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.ExceptionHandled || context.Exception is null)
        {
            return;
        }

        if (context.ActionDescriptor.EndpointMetadata.FirstOrDefault(x => 
            x is HandleExceptionAttribute handleAttr &&
            handleAttr.ExceptionType.IsInstanceOfType(context.Exception)) is  HandleExceptionAttribute handleExceptionAttr)
        {
            var problemDetails = new ProblemDetails
            {
                Title = "Fout in applicatie",
                Status = (int)System.Net.HttpStatusCode.InternalServerError,
                Detail = $"{context.Exception.Source};{context.Exception.Message};{context.Exception.InnerException?.Message}"
            };

            context.HttpContext.Response.StatusCode = (int)problemDetails.Status;
            context.Result = new ObjectResult(problemDetails);

            // Prevent the defaukt exception handling
            context.ExceptionHandled = true;
        }
    }
}
