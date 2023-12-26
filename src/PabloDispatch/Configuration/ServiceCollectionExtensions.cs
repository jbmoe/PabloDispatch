using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PabloCache.Abstractions.Configuration;

namespace PabloDispatch.Configuration;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the default PabloDispatch services to the service collection and provides configuration for adding command and query handlers.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="componentConfig"></param>
    /// <returns></returns>
    public static IServiceCollection AddPabloDispatch(this IServiceCollection services, Action<IPabloDispatchComponent>? componentConfig = null)
    {
        var component = new PabloDispatchComponent();

        componentConfig?.Invoke(component);

        services.Add(component.GetServices());

        services.AddPabloCaching(cache =>
        {
            component.PabloCacheConfig.Invoke(cache);
        });

        return services;
    }
}