using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Rutracker.Server.BusinessLayer.Exceptions;

namespace Rutracker.Server.WebApi.Filters
{
    public class ControllerExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger<ControllerExceptionFilterAttribute> _logger;

        private readonly Dictionary<ExceptionEventTypes, int> _statusCodesMapper =
            new Dictionary<ExceptionEventTypes, int>
            {
                [ExceptionEventTypes.NotFound] = StatusCodes.Status404NotFound,
                [ExceptionEventTypes.InvalidParameters] = StatusCodes.Status400BadRequest,
                [ExceptionEventTypes.LoginFailed] = StatusCodes.Status400BadRequest,
                [ExceptionEventTypes.RegistrationFailed] = StatusCodes.Status400BadRequest
            };

        public ControllerExceptionFilterAttribute(ILogger<ControllerExceptionFilterAttribute> logger)
        {
            _logger = logger;
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
#if DEBUG
            var message = context.Exception.Message;
#else
            var message = "Something went wrong on the server...";
#endif
            var statusCode = StatusCodes.Status500InternalServerError;

            if (context.Exception is RutrackerException exception)
            {
                message = exception.Message;

                if (_statusCodesMapper.ContainsKey(exception.ExceptionEventType))
                {
                    statusCode = _statusCodesMapper[exception.ExceptionEventType];
                }
            }

            context.Result = new ObjectResult(message)
            {
                StatusCode = statusCode
            };

            _logger.LogError(context.Exception, context.ActionDescriptor.ToString());
        }
    }
}