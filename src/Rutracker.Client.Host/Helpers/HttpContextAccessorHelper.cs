using Microsoft.AspNetCore.Http;

namespace Rutracker.Client.Host.Helpers
{
    public static class HttpContextAccessorHelper
    {
        public static bool IsServerStarted(this IHttpContextAccessor accessor)
        {
            return accessor?.HttpContext?.Response.HasStarted ?? false;
        }
    }
}