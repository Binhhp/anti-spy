using Newtonsoft.Json;

public class ResponseResult<T>
{
    [JsonProperty("success")]
    public bool Success { get; set; }
    [JsonProperty("error")]
    public ErrorDetail Error { get; set; }
    [JsonProperty("data")]
    public T Data { get; set; }
    public ResponseResult()
    {
        Success = true;
    }
    public ResponseResult(T data)
    {
        Success = true;
        Data = data;
    }

    public class ErrorDetail
    {
        [JsonProperty("error_code")]
        public string ErrorCode { get; set; }
        [JsonProperty("error_message")]
        public string ErrorMessage { get; set; }
        [JsonProperty("logid")]
        public string LogId { get; set; }
    }
    public ResponseResult<T> WihError(string error_code, string error_message)
    {
        var logId = Guid.NewGuid().ToString();
        Success = false;
        Error = new ErrorDetail
        {
            ErrorCode = error_code,
            ErrorMessage = error_message,
            LogId = logId
        };
        return this;
    }

    public static ResponseResult<T> WithError(string msg)
    {
        return new ResponseResult<T>().WihError("bad_request", msg);
    }
    public static ResponseResult<T> WithSuccess()
    {
        return new ResponseResult<T>();
    }
    public static ResponseResult<T> WithData(T data)
    {
        return new ResponseResult<T>(data);
    }
}