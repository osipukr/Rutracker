using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace Rutracker.Client.Blazor.Helpers
{
    public static class AuthenticationStateExtension
    {
        public static bool IsUserAuthenticated(this AuthenticationState state)
        {
            return state.User.Identity.IsAuthenticated;
        }

        public static bool IsUserInRole(this AuthenticationState state, string role)
        {
            return state.User.IsInRole(role);
        }

        public static string UserName(this AuthenticationState state)
        {
            return state.User.Identity.Name;
        }

        public static string UserId(this AuthenticationState state)
        {
            return state.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}