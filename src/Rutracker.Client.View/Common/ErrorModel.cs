using System;

namespace Rutracker.Client.View.Common
{
    public class ErrorModel
    {
        private ErrorModel()
        {
            IsError = false;
            Message = string.Empty;
        }

        public bool IsError { get; set; }
        public string Message { get; set; }

        public void Handler(string message)
        {
            IsError = true;
            Message = message;
        }

        public void HandlerException(Exception exception)
        {
            Handler(exception.Message);
        }

        public static ErrorModel Create()
        {
            return new ErrorModel();
        }
    }
}