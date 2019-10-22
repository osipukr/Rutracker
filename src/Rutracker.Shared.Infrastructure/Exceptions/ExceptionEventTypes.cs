namespace Rutracker.Shared.Infrastructure.Exceptions
{
    public enum ExceptionEventTypes : byte
    {
        NotFound = 1,
        InvalidParameters = 2,
        LoginFailed = 3,
        RegistrationFailed = 4
    }
}