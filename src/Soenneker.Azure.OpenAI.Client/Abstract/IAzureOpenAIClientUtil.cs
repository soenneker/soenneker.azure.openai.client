using Azure.AI.OpenAI;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace Soenneker.Azure.OpenAI.Client.Abstract;

/// <summary>
/// An async thread-safe singleton for the Azure OpenAI client
/// </summary>
public interface IAzureOpenAIClientUtil : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Not required, but can be used to set the options for the client
    /// </summary>
    /// <param name="options"></param>
    void SetOptions(AzureOpenAIClientOptions options);

    ValueTask<AzureOpenAIClient> Get(CancellationToken cancellationToken = default);
}