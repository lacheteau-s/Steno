using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Migrations.Services;

internal sealed class DatabaseManager(
    ILogger<DatabaseManager> logger,
    DatabaseInitializer dbInitializer)
{
    private readonly ILogger<DatabaseManager> _logger = logger;
    private readonly DatabaseInitializer _dbInitializer = dbInitializer;

    public async Task RunAsync(CancellationToken ct = default)
    {
        _logger.LogInformation("Initializing database...");

        await _dbInitializer.InitializeDatabase(ct);

        _logger.LogInformation("Database initialized");
    }
}
