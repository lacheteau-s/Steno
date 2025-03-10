using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Migrations.Services;

internal sealed class DatabaseUpdater(ILogger<DatabaseUpdater> logger, IConfiguration config)
{
    private readonly ILogger<DatabaseUpdater> _logger = logger;
    private readonly string _connectionString = config.GetConnectionString(CONNECTION_STRING_NAME)!;

    private const string CONNECTION_STRING_NAME = "Database";
    private const string VERIFY_SCHEMA_VERSION_QUERY = "SELECT OBJECT_ID('SchemaVersion', 'U')";
    private const string GET_CURRENT_SCHEMA_VERSION_QUERY = "SELECT MAX(version) FROM SchemaVersion";

    public async Task UpdateDatabase(CancellationToken ct)
    {
        var currentVersion = await GetCurrentVersion(ct);

        _logger.LogInformation("Current database version: {currentVersion}", currentVersion);
    }

    private async Task<int?> GetCurrentVersion(CancellationToken ct)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(ct);

        var version = await connection.ExecuteScalarAsync<int?>(VERIFY_SCHEMA_VERSION_QUERY);

        if (version != null)
            version = await connection.ExecuteScalarAsync<int>(GET_CURRENT_SCHEMA_VERSION_QUERY);

        return version;
    }
}
