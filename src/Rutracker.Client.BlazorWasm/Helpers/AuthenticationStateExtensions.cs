using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace Rutracker.Client.BlazorWasm.Helpers
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

        public static string GetUserName(this AuthenticationState state)
        {
            return state.User.Identity.Name;
        }

        public static string GetUserId(this AuthenticationState state)
        {
            return state.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}