internal static class ConfigurationExtensions
{
  public static void ConfigureServices(this IHostApplicationBuilder builder)
  {
    builder.Services.AddCors(options =>
    {
      options.AddDefaultPolicy(policy =>
      {
        // TODO
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
      });
    });

    // Add services to the container.
    // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
    builder.Services.AddOpenApi();
  }

  public static void ConfigureMiddlewares(this WebApplication app)
  {
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
      app.MapOpenApi();
    }

    app.UseHttpsRedirection();
    app.UseCors();
  }

  public static void ConfigureEndpoints(this WebApplication app)
  {
    app.MapWeatherForecastEndpoints();
  }
}