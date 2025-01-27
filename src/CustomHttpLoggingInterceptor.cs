using Microsoft.AspNetCore.HttpLogging;

public class CustomHttpLoggingInterceptor : IHttpLoggingInterceptor
{
    public ValueTask OnRequestAsync(HttpLoggingInterceptorContext logContext)
    {
        //if (logContext.HttpContext.Request.Path.StartsWithSegments("/swagger"))
        //{
        //    logContext.LoggingFields = HttpLoggingFields.None;
        //}


        logContext.AddParameter("RemoteIpAddress", logContext.HttpContext.Connection?.RemoteIpAddress?.ToString());

        return ValueTask.CompletedTask;
    }

    public ValueTask OnResponseAsync(HttpLoggingInterceptorContext logContext) => ValueTask.CompletedTask;
}