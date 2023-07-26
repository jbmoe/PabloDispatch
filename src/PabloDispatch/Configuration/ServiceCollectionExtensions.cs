using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

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

        return services;
    }
}