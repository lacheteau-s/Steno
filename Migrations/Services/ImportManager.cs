using Microsoft.Extensions.Logging;

namespace Migrations.Services;

public partial class ImportManager(ILogger<ImportManager> logger, BlobReader blobReader)
{
    private readonly ILogger<ImportManager> _logger = logger;
    private readonly BlobReader _reader = blobReader;

    public async Task RunAsync(CancellationToken ct = default)
    {
        await foreach (var file in _reader.GetFiles(ct))
        {
            _logger.LogInformation("Processing file '{fileName}'", file);

            var content = await _reader.ReadFile(file, ct);
        }
    }
}
