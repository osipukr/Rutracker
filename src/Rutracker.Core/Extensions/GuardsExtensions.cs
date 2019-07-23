﻿using Ardalis.GuardClauses;
using Rutracker.Core.Exceptions;

namespace Rutracker.Core.Extensions
{
    public static class GuardsExtensions
    {
        public static void Null<T>(this IGuardClause guardClause, string paramName, T input)
            where T : class
        {
            if (input == null)
            {
                throw new TorrentException($"The {paramName} not found.", ExceptionEvent.NotFound);
            }
        }

        public static void OutOfRange(this IGuardClause guardClause, string paramName, long input, long rangeFrom, long rangeTo)
        {
            if (input < rangeFrom || input > rangeTo)
            {
                throw new TorrentException($"The {paramName} is out of range ({rangeFrom} - {rangeTo}).", ExceptionEvent.NotValidParameters);
            }
        }
    }
}