using System.Net.Mime;
using System.Net;
using Newtonsoft.Json;

public class ExceptionLoggingMiddleware(RequestDelegate _next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            var errorMsg = ex.Message;
            if (ex.InnerException != null)
            {
                errorMsg = ex.InnerException.Message;
                if (ex.InnerException.InnerException != null)
                {
                    errorMsg = ex.InnerException.InnerException.Message;
                }
            }
            var response = new ResponseResult<object>().WihError("bad_request", errorMsg + ex.StackTrace);
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response, Formatting.Indented));
        }
    }
}