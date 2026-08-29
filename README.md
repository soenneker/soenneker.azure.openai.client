[![](https://img.shields.io/nuget/v/soenneker.azure.openai.client.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.azure.openai.client/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.azure.openai.client/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.azure.openai.client/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.azure.openai.client.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.azure.openai.client/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.azure.openai.client/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.azure.openai.client/actions/workflows/codeql.yml)

# Soenneker.Azure.OpenAI.Client

An async thread-safe singleton for the Azure OpenAI client.

## Install

```bash
dotnet add package Soenneker.Azure.OpenAI.Client
```

## Quick start

```csharp
using Soenneker.Azure.OpenAI.Client.Registrars;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
var result = services.AddAzureOpenAIClientUtilAsSingleton();
```

Adds `IAzureOpenAIClientUtil` as a singleton service.

## What you get

- `IAzureOpenAIClientUtil` — An async thread-safe singleton for the Azure OpenAI client.
- `AzureOpenAIClientUtilRegistrar` — An async thread-safe singleton for the Azure OpenAI client.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `AzureOpenAIClientUtilRegistrar.AddAzureOpenAIClientUtilAsSingleton(services)` | Adds `IAzureOpenAIClientUtil` as a singleton service. | The same service collection, so additional registrations can be chained. |
| `AzureOpenAIClientUtilRegistrar.AddAzureOpenAIClientUtilAsScoped(services)` | Adds `IAzureOpenAIClientUtil` as a scoped service. | The same service collection, so additional registrations can be chained. |

## Practical notes

- Reuse the registered client instead of constructing one per operation.
- Calls that return a cached or singleton value reuse the same instance until the owning service is disposed.
- Dispose instances you own when their scope ends so held resources can be released.
