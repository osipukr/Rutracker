using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace Rutracker.Core.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetError(this IdentityResult result)
        {
            var errors = result.Errors.Select(x => x.Description);

            return string.Join(Environment.NewLine, errors);
        }
    }
}