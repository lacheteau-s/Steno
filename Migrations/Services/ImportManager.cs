using Microsoft.Extensions.Logging;

namespace Migrations.Services;

public class ImportManager(ILogger<ImportManager> logger)
{
    private readonly ILogger<ImportManager> _logger = logger;

    public Task RunAsync()
    {
        _logger.LogInformation($"Importing data");
        return Task.CompletedTask;
    }
}
