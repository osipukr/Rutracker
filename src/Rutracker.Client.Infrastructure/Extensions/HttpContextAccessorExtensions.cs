using Microsoft.AspNetCore.Http;

namespace Rutracker.Client.Infrastructure.Extensions
{
    public static class HttpContextAccessorExtensions
    {
        public static bool IsServerStarted(this IHttpContextAccessor accessor)
        {
            return accessor?.HttpContext?.Response.HasStarted ?? false;
        }
    }
}