public static partial class Log
{
    [LoggerMessage(
        Level = LogLevel.Information,
        Message = "Generated weather forecasts")]
    public static partial void GeneratedWeatherForecasts(
       this ILogger logger, WeatherForecast[] weatherForecasts);
}
