using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Migrations.Services;

internal sealed class DatabaseInitializer
{
    private readonly ILogger<DatabaseInitializer> _logger;
    private readonly SqlConnectionStringBuilder _connectionStringBuilder;
    private readonly string _dbName;

    private const string CONNECTION_STRING_NAME = "Database";
    private const string DB_EXISTS_QUERY = "SELECT DB_ID(@dbName)";
    private const string CREATE_DB_QUERY = "EXEC('CREATE DATABASE ' + @dbName)";

    public DatabaseInitializer(ILogger<DatabaseInitializer> logger, IConfiguration config)
    {
        _logger = logger;

        var connectionString = config.GetConnectionString("Database");

        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException($"Missing connection string for '{CONNECTION_STRING_NAME}'");
        
        _connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
        _dbName = _connectionStringBuilder.InitialCatalog;

        if (string.IsNullOrWhiteSpace(_dbName))
            throw new InvalidOperationException("Initial catalog is missing in connection string");

        _connectionStringBuilder.InitialCatalog = string.Empty;
    }

    public async Task InitializeDatabase(CancellationToken ct)
    {
        await using var connection = new SqlConnection(_connectionStringBuilder.ConnectionString);
        await connection.OpenAsync(ct);

        var id = await connection.ExecuteScalarAsync(DB_EXISTS_QUERY, new { dbName = _dbName });

        if (id == null)
        {
            _logger.LogInformation("Database '{dbName}' does not exists, creating...", _dbName);
            await connection.ExecuteAsync(CREATE_DB_QUERY, new { dbName = _dbName });
        }
    }
}
