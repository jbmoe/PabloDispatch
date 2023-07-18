using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace PabloDispatch.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPabloDispatch(this IServiceCollection services, Action<IPabloDispatchComponent>? componentConfig = null)
    {
        var component = new PabloDispatchComponent();

        componentConfig?.Invoke(component);

        services
            .Add(component.Services.Values)
            .Add(component.RequestHandlers.Values)
            .Add(component.RequestPipelineHandlers)
            .Add(component.RequestPipelineProviders.Values);

        return services;
    }
}