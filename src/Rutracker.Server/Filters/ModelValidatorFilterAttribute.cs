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
            if (!context.ModelState.IsValid)
            {
                var error = context.ModelState.Values
                    .SelectMany(x => x.Errors)
                    .FirstOrDefault()
                    ?.ErrorMessage;

                throw new TorrentException(error, ExceptionEvent.NotValidParameters);
            }
        }
    }
}