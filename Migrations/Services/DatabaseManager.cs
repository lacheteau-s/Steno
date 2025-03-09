using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Migrations.Services;

internal sealed class DatabaseManager(ILogger<DatabaseManager> logger, IConfiguration config)
{
    private readonly ILogger<DatabaseManager> _logger = logger;
    private readonly IConfiguration _config = config;

    public void Run()
    {
        _logger.LogInformation("Initializing database...");

        Console.WriteLine(_config.GetConnectionString("Database"));

        _logger.LogInformation("Database initialized");
    }
}
