using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Migrations.Services;

internal sealed class DatabaseManager(
    ILogger<DatabaseManager> logger,
    DatabaseInitializer dbInitializer,
    DatabaseUpdater dbUpdater)
{
    private readonly ILogger<DatabaseManager> _logger = logger;
    private readonly DatabaseInitializer _dbInitializer = dbInitializer;
    private readonly DatabaseUpdater _dbUpdater = dbUpdater;

    public async Task RunAsync(CancellationToken ct = default)
    {
        _logger.LogInformation("Initializing database...");

        await _dbInitializer.InitializeDatabase(ct);
        await _dbUpdater.UpdateDatabase(ct);

        _logger.LogInformation("Database initialized");
    }
}
