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
public class AzureOpenAIClientUtil: IAzureOpenAIClientUtil
{
    private readonly AsyncSingleton<AzureOpenAIClient> _client;

    private AzureOpenAIClientOptions? _options;

    public AzureOpenAIClientUtil(ILogger<AzureOpenAIClient> logger, IConfiguration configuration)
    {
        _client = new AsyncSingleton<AzureOpenAIClient>(() =>
        {
            var uri = configuration.GetValueStrict<string>("Azure:OpenAI:Uri");
            var apiKey = configuration.GetValueStrict<string>("Azure:OpenAI:ApiKey");

            logger.LogDebug("Creating Azure OpenAI client ({uri})...", uri);

            var credential = new ApiKeyCredential(apiKey);

            var client = new AzureOpenAIClient(new Uri(uri), credential, _options);

            return client;
        });
    }

    public void SetOptions(AzureOpenAIClientOptions options)
    {
        _options = options;
    }

    public ValueTask<AzureOpenAIClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);

        _client.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);

        return _client.DisposeAsync();
    }
}
