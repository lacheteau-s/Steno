using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Migrations.Entities;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Migrations.Services;

public partial class ImportManager(ILogger<ImportManager> logger, BlobReader blobReader, IConfiguration config)
{
    private readonly ILogger<ImportManager> _logger = logger;
    private readonly BlobReader _reader = blobReader;
    private readonly string _connectionString = config.GetConnectionString("Database")
        ?? throw new InvalidOperationException("Missing connection string for 'Database'");

    public async Task RunAsync(CancellationToken ct = default)
    {
        var notes = await ProcessFiles(ct);
        await Save(notes);
    }

    private async Task Save(IEnumerable<Note> notes)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var filteredNotes = await FilterExistingNotes(connection, notes);

        if (filteredNotes == null)
        {
            _logger.LogInformation("Nothing to import");
            return;
        }

        _logger.LogInformation("Saving {notesCount} blobs entries to database...", filteredNotes.Count());

        await connection.ExecuteAsync(
            "INSERT INTO Notes (Content, CreatedAt, ImportChecksum) VALUES (@Content, @CreatedAt, @ImportChecksum)",
            notes
        );
    }

    private async Task<IEnumerable<Note>?> FilterExistingNotes(SqlConnection connection, IEnumerable<Note> notes)
    {
        _logger.LogInformation("Checking database for existing blob contents...");

        var checksums = string.Join(",", notes.Select(n => $"'{n.ImportChecksum}'"));
        var existing = await connection.QueryAsync<string>(
            $"SELECT ImportChecksum FROM Notes WHERE ImportChecksum IN ({checksums})"
        ) ?? throw new InvalidOperationException("An error has occurred");
        
        if (existing.Count() == 0)
            return notes;

        _logger.LogInformation("Found {existingNotesCount} entries", existing.Count());

        var filteredNotes = notes.Where(n => !existing.Contains(n.ImportChecksum));

        if (filteredNotes.Any())
            return filteredNotes;
        else return null;
    }

    private async Task<IEnumerable<Note>> ProcessFiles(CancellationToken ct)
    {
        var notes = new List<Note>();
        await foreach (var file in _reader.GetFiles(ct))
        {
            _logger.LogInformation("Processing file '{fileName}'", file);

            var content = await _reader.ReadFile(file, ct);
            notes.AddRange(Process(content));
        }

        return notes;
    }

    private IEnumerable<Note> Process(string content)
    {
        var matches = EntryRegex().Matches(content);
        var entities = matches.Select((m, i) =>
        {
            var hash = SHA256.HashData(Encoding.UTF8.GetBytes(m.Groups[0].Value));
            var createdAt = DateTime.Parse(m.Groups["CreatedAt"].Value);
            var content = m.Groups["Content"].Value.Trim();

            return new Note
            {
                ImportChecksum = string.Join("", hash.Select(x => x.ToString("X2"))),
                CreatedAt = TimeZoneInfo.ConvertTimeToUtc(createdAt, TimeZoneInfo.Local),
                Content = content
            };
        });

        return entities;
    }

    [GeneratedRegex(@"(?<CreatedAt>\d{4}\/\d{2}\/\d{2} \d{1,2}:\d{2} (?:AM|PM)): (?<Content>.+)", RegexOptions.Compiled)]
    private static partial Regex EntryRegex();
}
