using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Migrations.Services;

internal sealed class DatabaseUpdater(
    ILogger<DatabaseUpdater> logger,
    IConfiguration config,
    SqlScriptsProvider sqlScriptsProvider)
{
    private readonly ILogger<DatabaseUpdater> _logger = logger;
    private readonly string _connectionString = config.GetConnectionString(CONNECTION_STRING_NAME)!;
    private readonly SqlScriptsProvider _sqlScriptsProvider = sqlScriptsProvider;

    private const string CONNECTION_STRING_NAME = "Database";
    private const string VERIFY_SCHEMA_VERSION_QUERY = "SELECT OBJECT_ID('SchemaVersion', 'U')";
    private const string GET_CURRENT_SCHEMA_VERSION_QUERY = "SELECT MAX(version) FROM SchemaVersion";
    private const string INSERT_SCHEMA_VERSION_QUERY = "INSERT INTO SchemaVersion VALUES (@Version, @FileName, @CreatedAt)";

    public async Task UpdateDatabase(CancellationToken ct)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(ct);

        var currentVersion = await GetCurrentVersion(connection);
        var expectedVersion = GetExpectedVersion();
        var isUpToDate = IsDatabaseUpToDate(currentVersion, expectedVersion);

        if (!isUpToDate)
            await ApplyMigrations(connection, currentVersion, ct);
    }

    private async Task ApplyMigrations(SqlConnection connection, int? currentVersion, CancellationToken ct)
    {
        currentVersion ??= -1;

        foreach (var script in _sqlScriptsProvider.GetScripts().Where(x => x.Version > currentVersion))
        {
            if (currentVersion - script.Version > 1)
                throw new InvalidOperationException($"Missing migration script for version {currentVersion + 1}");

            _logger.LogInformation("Applying script '{fileName}' for version {currentVersion}", script.FileInfo.Name, script.Version);

            var query = await File.ReadAllTextAsync(script.FileInfo.PhysicalPath!, ct);

            await using var transaction = await connection.BeginTransactionAsync(ct);

            await connection.ExecuteAsync(query, transaction: transaction);
            await connection.ExecuteAsync(INSERT_SCHEMA_VERSION_QUERY, new
            {
                version = script.Version,
                fileName = script.FileInfo.Name,
                createdAt = DateTime.UtcNow
            }, transaction);

            await transaction.CommitAsync(ct);

            ++currentVersion;

            _logger.LogInformation("Success!");
        }

        _logger.LogInformation("Database is up-to-date. Version: {currentVersion}", currentVersion);
    }

    private bool IsDatabaseUpToDate(int? currentVersion, int expectedVersion)
    {
        if (currentVersion == null || currentVersion < expectedVersion)
            _logger.LogWarning("Database is out of date. Expected version: {expectedVersion}. Actual version: {currentVersion}", expectedVersion, currentVersion);
        else if (currentVersion == expectedVersion)
            _logger.LogInformation("Database is up-to-date. Version: {currentVersion}", currentVersion);
        else if (currentVersion > expectedVersion)
            throw new InvalidOperationException($"Database version ({currentVersion}) is ahead of target ({expectedVersion}). The application was likely downgraded");

        return currentVersion == expectedVersion;
    }

    private static async Task<int?> GetCurrentVersion(SqlConnection connection)
    {
        var version = await connection.ExecuteScalarAsync<int?>(VERIFY_SCHEMA_VERSION_QUERY);

        if (version != null)
            version = await connection.ExecuteScalarAsync<int>(GET_CURRENT_SCHEMA_VERSION_QUERY);

        return version;
    }

    private int GetExpectedVersion()
    {
        var version = _sqlScriptsProvider.GetScripts().Select(x => x.Version).LastOrDefault(-1);

        if (version < 0)
            throw new InvalidOperationException("Unable to determine expected database version. No migration scripts found");

        return version;
    }
}
