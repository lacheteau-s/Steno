
using Microsoft.Extensions.Configuration;

namespace Mobile.Configuration;

internal sealed class AzureFunctionKeyHeaderHandler : DelegatingHandler
{
    private readonly string? _apiKey;

    public AzureFunctionKeyHeaderHandler(IConfiguration config)
    {
        _apiKey = config.GetValue<string>("Api:ApiKey");
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(_apiKey))
            request.Headers.Add("x-functions-key", _apiKey);

        return base.SendAsync(request, cancellationToken);
    }
}
