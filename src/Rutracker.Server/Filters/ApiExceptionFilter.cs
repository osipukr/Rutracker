using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Rutracker.Core.Exceptions;
using Rutracker.Server.Response;

namespace Rutracker.Server.Filters
{
    public class ApiExceptionFilter : IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            IActionResult result;

            if (context.Exception is GenericException exception)
            {
                var message = exception.Message;
                var statusCode = exception.ExceptionEvent switch
                {
                    ExceptionEvent.NotFound => StatusCodes.Status404NotFound,
                    ExceptionEvent.NotValidParameters => StatusCodes.Status400BadRequest
                };

                var response = new BadRequestResponse(message, statusCode);

                result = new OkObjectResult(response);
            }
            else
            {
                var response = new BadRequestResponse(context.Exception.Message,
                    StatusCodes.Status500InternalServerError);

                result = new BadRequestObjectResult(response);
            }

            context.Result = result;
            context.ExceptionHandled = true;

            return Task.CompletedTask;
        }
    }
}