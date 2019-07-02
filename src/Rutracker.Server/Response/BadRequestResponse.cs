namespace Rutracker.Server.Response
{
    public class BadRequestResponse : BaseResponse
    {
        public int StatusCode { get; protected set; }

        public BadRequestResponse(string message, int statusCode)
            : base(false, message)
        {
            StatusCode = statusCode;
        }
    }
}