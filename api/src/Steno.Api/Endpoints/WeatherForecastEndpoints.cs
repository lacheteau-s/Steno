internal static class WeatherForecastEndpoints
{
  private static readonly string[] summaries =
  [
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
  ];

  public static void MapWeatherForecastEndpoints(this IEndpointRouteBuilder endpoints)
  {
    endpoints.MapGet("/weatherforecast", GetWeatherForecast)
      .WithName("GetWeatherForecast");
  }

  private static WeatherForecast[] GetWeatherForecast()
  {
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();

    return forecast;
  }
}

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}