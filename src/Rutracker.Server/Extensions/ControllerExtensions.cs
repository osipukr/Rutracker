using System;
using Microsoft.AspNetCore.Mvc;

namespace Rutracker.Server.Extensions
{
    public static class ControllerExtensions
    {
        public static string ControllerName(this Type controllerType)
        {
            var baseType = typeof(ControllerBase);

            if (!baseType.IsAssignableFrom(controllerType))
            {
                return controllerType.Name;
            }

            var lastIndex = controllerType.Name.LastIndexOf("Controller", StringComparison.Ordinal);

            return lastIndex > 0
                ? controllerType.Name.Substring(0, lastIndex)
                : controllerType.Name;
        }
    }
}