namespace Rutracker.Server.Response
{
    public class BadRequestResponse : BaseResponse
    {
        public BadRequestResponse(string message) 
            : base(false, message)
        {
        }
    }
}