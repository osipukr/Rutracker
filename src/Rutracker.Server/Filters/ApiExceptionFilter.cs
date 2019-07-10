using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Rutracker.Core.Exceptions;

namespace Rutracker.Server.Filters
{
    public class ApiExceptionFilter : IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            var message = "Something went wrong on the server...";
            var statusCode = StatusCodes.Status500InternalServerError;

            if (context.Exception is TorrentException exception)
            {
                message = exception.Message;
                statusCode = exception.ExceptionEvent switch
                {
                    ExceptionEvent.NotFound => StatusCodes.Status404NotFound,
                    ExceptionEvent.NotValidParameters => StatusCodes.Status400BadRequest
                };
            }

            context.Result = new ObjectResult(message)
            {
                StatusCode = statusCode
            };

            return Task.CompletedTask;
        }
    }
}