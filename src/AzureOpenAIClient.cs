using System;
using System.ClientModel;
using Azure.AI.OpenAI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Soenneker.Azure.OpenAI.Client.Abstract;
using Soenneker.Extensions.Configuration;
using Soenneker.Utils.AsyncSingleton;
using Soenneker.Extensions.String;
using System.Threading.Tasks;
using System.Threading;

namespace Soenneker.Azure.OpenAI.Client;

/// <inheritdoc cref="IAzureOpenAIClientUtil"/>
public class AzureOpenAIClientUtil: IAzureOpenAIClientUtil
{
    private readonly AsyncSingleton<AzureOpenAIClient> _client;

    private AzureOpenAIClientOptions? _options;
    private string? _deployment;

    public AzureOpenAIClientUtil(ILogger<AzureOpenAIClient> logger, IConfiguration configuration)
    {
        _client = new AsyncSingleton<AzureOpenAIClient>(() =>
        {
            var uri = configuration.GetValueStrict<string>("Azure:OpenAI:Uri");
            var apiKey = configuration.GetValueStrict<string>("Azure:OpenAI:ApiKey");
            var deployment = configuration.GetValue<string?>("Azure:OpenAI:Deployment");

            if (!_deployment.IsNullOrEmpty())
                deployment = _deployment;

            logger.LogDebug("Creating Azure OpenAI client with deployment ({deployment})...", deployment);

            var credential = new ApiKeyCredential(apiKey);

            var client = new AzureOpenAIClient(new Uri(uri), credential, _options);

            return client;
        });
    }

    public void SetOptions(string model, AzureOpenAIClientOptions options)
    {
        _deployment = model;
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
