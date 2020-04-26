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

        public void HandlerException(Exception exception)
        {
            IsError = true;
            Message = exception.Message;
        }

        public static ErrorModel Create()
        {
            return new ErrorModel();
        }
    }
}