using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Rutracker.Server.WebApi.Extensions
{
    public static class ModelStateExtensions
    {
        public static string GetError(this ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
            var message = string.Join(Environment.NewLine, errors);

            return message;
        }
    }
}