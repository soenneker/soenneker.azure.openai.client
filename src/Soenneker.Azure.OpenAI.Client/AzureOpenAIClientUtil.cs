using System;
using System.ClientModel;
using Azure.AI.OpenAI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Soenneker.Azure.OpenAI.Client.Abstract;
using Soenneker.Extensions.Configuration;
using Soenneker.Utils.AsyncSingleton;
using System.Threading.Tasks;
using System.Threading;

namespace Soenneker.Azure.OpenAI.Client;

/// <inheritdoc cref="IAzureOpenAIClientUtil"/>
public sealed class AzureOpenAIClientUtil: IAzureOpenAIClientUtil
{
    private readonly AsyncSingleton<AzureOpenAIClient> _client;
    private readonly ILogger<AzureOpenAIClient> _logger;
    private readonly IConfiguration _configuration;
    private readonly object _optionsLock = new();

    private AzureOpenAIClientOptions? _options;
    private bool _clientCreated;

    public AzureOpenAIClientUtil(ILogger<AzureOpenAIClient> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
        _client = new AsyncSingleton<AzureOpenAIClient>(CreateClient);
    }

    private AzureOpenAIClient CreateClient()
    {
        var uri = _configuration.GetValueStrict<string>("Azure:OpenAI:Uri");
        var apiKey = _configuration.GetValueStrict<string>("Azure:OpenAI:ApiKey");

        _logger.LogDebug("Creating Azure OpenAI client ({uri})...", uri);

        if (!Uri.TryCreate(uri, UriKind.Absolute, out Uri? endpoint) || endpoint.Scheme != Uri.UriSchemeHttps)
            throw new InvalidOperationException("Azure:OpenAI:Uri must be an absolute HTTPS URI.");

        var credential = new ApiKeyCredential(apiKey);

        lock (_optionsLock)
        {
            var client = new AzureOpenAIClient(endpoint, credential, _options);
            _clientCreated = true;
            return client;
        }
    }

    public void SetOptions(AzureOpenAIClientOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);

        lock (_optionsLock)
        {
            if (_clientCreated)
                throw new InvalidOperationException("Options must be set before the Azure OpenAI client is created.");

            _options = options;
        }
    }

    public ValueTask<AzureOpenAIClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    /// <summary>
    /// Releases resources used by the current instance.
    /// </summary>
    public void Dispose()
    {
        _client.Dispose();
    }

    /// <summary>
    /// Asynchronously releases resources used by the current instance.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }
}
