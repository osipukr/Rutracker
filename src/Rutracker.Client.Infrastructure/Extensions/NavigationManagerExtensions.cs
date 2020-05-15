using System.ComponentModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;

namespace Rutracker.Client.Infrastructure.Extensions
{
    public static class NavigationManagerExtensions
    {
        public static bool TryGetQueryString<T>(this NavigationManager navigationManager, string key, out T value)
        {
            var uri = navigationManager.ToAbsoluteUri(navigationManager.Uri);

            try
            {
                if (QueryHelpers.ParseQuery(uri.Query).TryGetValue(key, out var valueFromQueryString))
                {
                    var converter = TypeDescriptor.GetConverter(typeof(T));

                    value = (T)converter.ConvertFromString(valueFromQueryString);

                    return true;
                }
            }
            catch
            {
                // ignored
            }

            value = default;

            return false;
        }
    }
}