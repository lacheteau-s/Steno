using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Migrations.Services;

namespace Migrations;

internal static class Startup
{
    public static void ConfigureDefaults(this IServiceCollection services)
    {
        services.AddSingleton<DatabaseManager>();
        services.AddSingleton<DatabaseInitializer>();
        services.AddSingleton<DatabaseUpdater>();
        services.AddSingleton<SqlScriptsProvider>();
        services.AddSingleton<IFileProvider>(CreateFileProvider);
    }

    private static IFileProvider CreateFileProvider(IServiceProvider sp)
    {
        var env = sp.GetRequiredService<IHostEnvironment>();
        var path = Path.Combine(env.ContentRootPath, "Scripts");

        return new PhysicalFileProvider(path);
    }
}
