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
    /// Sets client options before the Azure OpenAI client is first created.
    /// </summary>
    /// <param name="options">Options used to construct the Azure OpenAI client.</param>
    /// <exception cref="ArgumentNullException"><paramref name="options"/> is <see langword="null"/>.</exception>
    /// <exception cref="InvalidOperationException">The Azure OpenAI client has already been created.</exception>
    void SetOptions(AzureOpenAIClientOptions options);

    /// <summary>
    /// Returns the configured azure OpenAI Client used by the azure openai client.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested azure OpenAI Client.</returns>
    ValueTask<AzureOpenAIClient> Get(CancellationToken cancellationToken = default);
}
