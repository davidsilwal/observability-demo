using System.Diagnostics.Metrics;

public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

public class MetricCollectorMiddleware : IMiddleware
{
    private static readonly Meter _meter = new("MyAppMetrics", "1.0");

    // Define counters for tracking metrics
    private static readonly Counter<long> RequestCounter = _meter.CreateCounter<long>(
        "app.requests",
        description: "Counts total HTTP requests.");


    public MetricCollectorMiddleware(IMeterFactory meterFactory)
    {
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        finally
        {
            var userId = context.User.Identity?.Name ?? "Anonymous";
            var remoteIp = context.Connection.RemoteIpAddress?.ToString();
            var requestPath = context.Request.Path;
            var controllerName = context.GetRouteValue("controller")?.ToString() ?? "Unknown";
            var actionName = context.GetRouteValue("action")?.ToString() ?? "Unknown";

            // Record metrics
            RequestCounter.Add(1,
            [
                new("user_id", userId),
                new("remote_ip", remoteIp),
                new("controller", controllerName),
                new("action", actionName)
            ]);
        }
    }
}
