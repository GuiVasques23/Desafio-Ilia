
namespace Time.Sheet.Domain.Utils
{
    public class ResponseResult
    {
        public int StatusCode { get; set; }
        public string Content { get; set; }

        public ResponseResult(int statusCode, string content)
        {
            StatusCode = statusCode;
            Content = content;
        }
    }
}
