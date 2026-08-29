[![](https://img.shields.io/nuget/v/soenneker.azure.openai.client.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.azure.openai.client/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.azure.openai.client/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.azure.openai.client/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.azure.openai.client.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.azure.openai.client/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.azure.openai.client/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.azure.openai.client/actions/workflows/codeql.yml)

# Soenneker.Azure.OpenAI.Client

Creates and caches an API-key-authenticated `AzureOpenAIClient` for dependency-injected applications.

## Installation

```bash
dotnet add package Soenneker.Azure.OpenAI.Client
```

## Configuration

```json
{
  "Azure": {
    "OpenAI": {
      "Uri": "https://your-resource.openai.azure.com",
      "ApiKey": "your-api-key"
    }
  }
}
```

The endpoint must be an absolute HTTPS URI. Store the API key in a secret provider or environment variable, not source-controlled configuration.

## Registration and use

```csharp
using Azure.AI.OpenAI;
using Soenneker.Azure.OpenAI.Client.Abstract;
using Soenneker.Azure.OpenAI.Client.Registrars;

builder.Services.AddAzureOpenAIClientUtilAsSingleton();

public sealed class AzureOpenAIService(IAzureOpenAIClientUtil clientUtil)
{
    public ValueTask<AzureOpenAIClient> GetClient(
        CancellationToken cancellationToken) =>
        clientUtil.Get(cancellationToken);
}
```

Use the returned Azure client to obtain deployment-specific chat, audio, embedding, or other clients supported by the Azure/OpenAI SDK.

## Client options and lifecycle

Call `SetOptions()` before the first `Get()` when custom `AzureOpenAIClientOptions` are required:

```csharp
clientUtil.SetOptions(new AzureOpenAIClientOptions());
AzureOpenAIClient client = await clientUtil.Get(cancellationToken);
```

- The first successful `Get()` creates the client; later calls return the same instance.
- `SetOptions()` after creation throws because a cached client cannot be reconfigured.
- Endpoint, API key, and options changes require replacing the utility instance.
- Missing configuration or a non-HTTPS endpoint fails initialization.
- Let the DI container dispose registered instances.

For deployment-specific wrappers, see `Soenneker.Azure.OpenAI.Client.Chat` and `Soenneker.Azure.OpenAI.Client.Audio`.
