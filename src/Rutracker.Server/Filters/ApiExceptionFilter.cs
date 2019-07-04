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
            ObjectResult result;

            if (context.Exception is TorrentException exception)
            {
                result = new ObjectResult(exception.Message)
                {
                    StatusCode = exception.ExceptionEvent switch
                    {
                        ExceptionEvent.NotFound => StatusCodes.Status404NotFound,
                        ExceptionEvent.NotValidParameters => StatusCodes.Status400BadRequest
                    }
                };
            }
            else
            {
                result = new ObjectResult(context.Exception.Message)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }

            context.Result = result;

            return Task.CompletedTask;
        }
    }
}