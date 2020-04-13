using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Rutracker.Server.BusinessLayer.Exceptions;
using Rutracker.Server.WebApi.Extensions;

namespace Rutracker.Server.WebApi.Filters
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

            throw new RutrackerException(context.ModelState.GetError(), ExceptionEventTypes.InvalidParameters);
        }
    }
}