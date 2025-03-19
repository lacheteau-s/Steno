using Microsoft.Extensions.Logging;
using Migrations.Entities;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Migrations.Services;

public partial class ImportManager(ILogger<ImportManager> logger, BlobReader blobReader)
{
    private readonly ILogger<ImportManager> _logger = logger;
    private readonly BlobReader _reader = blobReader;

    public async Task RunAsync(CancellationToken ct = default)
    {
        var notes = await ProcessFiles(ct);
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
