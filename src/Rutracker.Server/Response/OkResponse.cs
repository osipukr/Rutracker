namespace Rutracker.Server.Response
{
    public class OkResponse<T> : BaseResponse
        where T : class
    {
        public T Value { get; protected set; }

        public OkResponse(T value) 
            : base(true, null)
        {
            Value = value;
        }
    }
}