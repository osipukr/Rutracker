namespace Rutracker.Client.Response
{
    public class OkResponse<T> : BaseResponse
    {
        public T Value { get; set; }
    }
}