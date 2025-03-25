using API.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;

namespace API;

internal static class Startup
{
    public static void ConfigureDefaults(this IServiceCollection services)
    {
        services
            .AddApplicationInsightsTelemetryWorkerService()
            .ConfigureFunctionsApplicationInsights();

        services.AddSingleton<NotesService>();
    }
}
