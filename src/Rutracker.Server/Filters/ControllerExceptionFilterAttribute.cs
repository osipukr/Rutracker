using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Rutracker.Core.Exceptions;

namespace Rutracker.Server.Filters
{
    public class ControllerExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            ExceptionHandler(context);

            base.OnException(context);
        }

        public override Task OnExceptionAsync(ExceptionContext context)
        {
            ExceptionHandler(context);

            return base.OnExceptionAsync(context);
        }

        private static void ExceptionHandler(ExceptionContext context)
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
        }
    }
}