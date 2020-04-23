using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace Rutracker.Client.Infrastructure.Extensions
{
    public static class AuthenticationStateExtensions
    {
        public static bool IsUserAuthenticated(this AuthenticationState state)
        {
            return state.User.Identity?.IsAuthenticated ?? false;
        }

        public static bool IsUserInRole(this AuthenticationState state, string role)
        {
            return state.User.IsInRole(role);
        }

        public static string GetUserId(this AuthenticationState state)
        {
            return state.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public static string GetUserName(this AuthenticationState state)
        {
            return state.User.Identity?.Name;
        }

        public static string GetUserImage(this AuthenticationState state)
        {
            return state.User.FindFirst(ClaimTypes.Uri)?.Value;
        }

        public static IEnumerable<string> GetUserRole(this AuthenticationState state)
        {
            return state.User.FindAll(ClaimTypes.Role).Select(x => x.Value);
        }
    }
}