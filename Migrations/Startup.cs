using Microsoft.Extensions.DependencyInjection;
using Migrations.Services;

namespace Migrations;

internal static class Startup
{
    public static void ConfigureDefaults(this IServiceCollection services)
    {
        services.AddSingleton<DatabaseManager>();
    }
}
