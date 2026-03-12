using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Azure.OpenAI.Client.Abstract;

namespace Soenneker.Azure.OpenAI.Client.Registrars;

/// <summary>
/// An async thread-safe singleton for the Azure OpenAI client
/// </summary>
public static class AzureOpenAIClientUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="IAzureOpenAIClientUtil"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddAzureOpenAIClientUtilAsSingleton(this IServiceCollection services)
    {
        services.TryAddSingleton<IAzureOpenAIClientUtil, AzureOpenAIClientUtil>();
        return services;
    }

    /// <summary>
    /// Adds <see cref="IAzureOpenAIClientUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddAzureOpenAIClientUtilAsScoped(this IServiceCollection services)
    {
        services.TryAddScoped<IAzureOpenAIClientUtil, AzureOpenAIClientUtil>();
        return services;
    }
}