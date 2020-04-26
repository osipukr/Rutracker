using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Rutracker.Server.BusinessLayer.Exceptions;

namespace Rutracker.Server.WebApi.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;

        private readonly Dictionary<ExceptionEventTypes, int> _statusCodesMapper =
            new Dictionary<ExceptionEventTypes, int>
            {
                [ExceptionEventTypes.NotFound] = StatusCodes.Status404NotFound,
                [ExceptionEventTypes.InvalidParameters] = StatusCodes.Status400BadRequest,
                [ExceptionEventTypes.LoginFailed] = StatusCodes.Status400BadRequest,
                [ExceptionEventTypes.RegistrationFailed] = StatusCodes.Status400BadRequest
            };

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            ExceptionHandler(context);
        }

        private void ExceptionHandler(ExceptionContext context)
        {
#if DEBUG
            var message = context.Exception.Message;
#else
            var message = "Something went wrong on the server...";
#endif
            var statusCode = StatusCodes.Status500InternalServerError;

            if (context.Exception is RutrackerException rutrackerException)
            {
                var exception = context.Exception;

                while (exception.InnerException != null)
                {
                    message += exception.InnerException.Message;

                    exception = exception.InnerException;
                }

                if (_statusCodesMapper.ContainsKey(rutrackerException.ExceptionEventType))
                {
                    statusCode = _statusCodesMapper[rutrackerException.ExceptionEventType];
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