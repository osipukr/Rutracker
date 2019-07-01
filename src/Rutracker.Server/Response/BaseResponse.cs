namespace Rutracker.Server.Response
{
    public abstract class BaseResponse
    {
        public bool IsSuccess { get; protected set; }
        public string Message { get; protected set; }

        protected BaseResponse(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }
    }
}