using System;
using System.Linq;

namespace Rutracker.Client.Host.Helpers
{
    public static class ObjectToQueryStringHelper
    {
        public static string ToQueryString(this object obj)
        {
            return string.Join("&", obj.GetType()
                .GetProperties()
                .Where(p => p.GetValue(obj, null) != null)
                .Select(p => $"{Uri.EscapeDataString(p.Name)}={Uri.EscapeDataString(p.GetValue(obj).ToString())}"));
        }
    }
}