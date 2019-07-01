namespace Rutracker.IntegrationTests.Response
{
    public class OkResponse<T> : BaseResponse
    {
        public T Value { get; set; }
    }
}