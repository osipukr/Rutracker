using Ardalis.GuardClauses;
using Rutracker.Core.Exceptions;

namespace Rutracker.Core.Extensions
{
    public static class GuardsExtensions
    {
        public static void Null<T>(this IGuardClause guardClause, string param, T input)
            where T : class
        {
            if (input == null)
            {
                throw new TorrentException($"{param} not found.", ExceptionEvent.NotFound);
            }
        }

        public static void OutOfRange(this IGuardClause guardClause, string param, long input, long rangeFrom, long rangeTo)
        {
            if (input < rangeFrom || input > rangeTo)
            {
                throw new TorrentException($"The {param} is out of range.", ExceptionEvent.NotValidParameters);
            }
        }
    }
}