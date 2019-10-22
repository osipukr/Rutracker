using Microsoft.AspNetCore.Identity;
using Rutracker.Server.BusinessLayer.Extensions;
using Rutracker.Shared.Infrastructure.Exceptions;

namespace Ardalis.GuardClauses
{
    public static class GuardClauseExtensions
    {
        public static void NullNotFound<T>(this IGuardClause guardClause, T input, string message) where T : class
        {
            Null(guardClause, input, message, ExceptionEventTypes.NotFound);
        }

        public static void NullNotValid<T>(this IGuardClause guardClause, T input, string message) where T : class
        {
            Null(guardClause, input, message, ExceptionEventTypes.InvalidParameters);
        }

        public static void OutOfRange(this IGuardClause guardClause, int input, int rangeFrom, int rangeTo, string message)
        {
            if (input < rangeFrom || input > rangeTo)
            {
                throw new RutrackerException(message, ExceptionEventTypes.InvalidParameters);
            }
        }

        public static void LessOne(this IGuardClause guardClause, int input, string message)
        {
            OutOfRange(guardClause, input, 1, int.MaxValue, message);
        }

        public static void NullOrWhiteSpace(this IGuardClause guardClause, string input, string message)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new RutrackerException(message, ExceptionEventTypes.InvalidParameters);
            }
        }

        public static void IsSucceeded(this IGuardClause guardClause, IdentityResult result)
        {
            if (!result.Succeeded)
            {
                throw new RutrackerException(result.GetError(), ExceptionEventTypes.InvalidParameters);
            }
        }

        private static void Null<T>(this IGuardClause guardClause, T input, string message, ExceptionEventTypes eventType)
            where T : class
        {
            if (input == null)
            {
                throw new RutrackerException(message, eventType);
            }
        }
    }
}