using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Rutracker.Server.Response;

namespace Rutracker.Server.Filters
{
    public class ApiExceptionFilterAttribute : Attribute, IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            var exceptionMessage = context.Exception.Message;

            context.Result = new OkObjectResult(new BadRequestResponse(exceptionMessage));
            context.ExceptionHandled = true;

            return Task.CompletedTask;
        }
    }
}