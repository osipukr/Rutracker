using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Rutracker.Core.Exceptions;

namespace Rutracker.Server.Filters
{
    public class ControllerExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger<ControllerExceptionFilterAttribute> _logger;

        public ControllerExceptionFilterAttribute(ILogger<ControllerExceptionFilterAttribute> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

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

        private void ExceptionHandler(ExceptionContext context)
        {
            var message = "Something went wrong on the server...";
            var statusCode = StatusCodes.Status500InternalServerError;

            if (context.Exception is TorrentException exception)
            {
                message = exception.Message;
                statusCode = exception.ExceptionEvent switch
                {
                    ExceptionEvent.NotFound => StatusCodes.Status404NotFound,
                    ExceptionEvent.NotValidParameters => StatusCodes.Status400BadRequest,
                    _ => StatusCodes.Status500InternalServerError
                };
            }

            context.Result = new ObjectResult(message)
            {
                StatusCode = statusCode
            };

            _logger.LogError(context.Exception, message: context.ActionDescriptor.ToString());
        }
    }
}