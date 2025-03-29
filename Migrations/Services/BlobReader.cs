
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

public class BlobReader
{
    private readonly ILogger<BlobReader> _logger;

    private readonly BlobServiceClient _blobServiceClient;

    private readonly BlobContainerClient _containerClient;

    public BlobReader(ILogger<BlobReader> logger, IConfiguration config)
    {
        _logger = logger;

        var connectionString = config.GetConnectionString("BlobStorage")
            ?? throw new InvalidOperationException("Missing connection string for 'BlobStorage'");

        _blobServiceClient = new BlobServiceClient(connectionString);
        _containerClient = _blobServiceClient.GetBlobContainerClient("import-files");
    }

    public async IAsyncEnumerable<string> GetFiles([EnumeratorCancellation] CancellationToken ct)
    {
        _logger.LogInformation("Fetching contents for blob container '{storageAccount}/{blobContainer}'", _containerClient.AccountName, _containerClient.Name);
        
        await foreach (var blob in _containerClient.GetBlobsAsync(cancellationToken: ct))
            yield return blob.Name;
    }

    public async Task<string> ReadFile(string file, CancellationToken ct)
    {
        _logger.LogInformation("Reading blob '{storageAccount}/{blobContainer}/{blobName}'", _containerClient.AccountName, _containerClient.Name, file);

        var blobClient = _containerClient.GetBlobClient(file);

        await using var stream = await blobClient.OpenReadAsync(cancellationToken: ct);
        using var reader = new StreamReader(stream);

        var content = await reader.ReadToEndAsync(cancellationToken: ct);

        return content;
    }
}
