using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Rutracker.Core.Exceptions;

namespace Rutracker.Server.Filters
{
    public class ModelValidatorFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ActionHandler(context);

            base.OnActionExecuting(context);
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            ActionHandler(context);

            return base.OnActionExecutionAsync(context, next);
        }

        private static void ActionHandler(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
            {
                return;
            }

            var errors = context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
            var message = string.Join(Environment.NewLine, errors);

            throw new TorrentException(message, ExceptionEventType.NotValidParameters);
        }
    }
}