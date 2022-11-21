using Microsoft.Extensions.DependencyInjection;

namespace Hemel.DependencyInjection.TypedFactory;

public static class ServiceCollectionEx
{
    public static IServiceCollection AddTypedFactory<TFactory>(this IServiceCollection services)
        where TFactory: notnull
    {
        services.Add(new ServiceDescriptor(
            typeof(TFactory),
            (serviceProvider) => TypedFactoryCreator.Create<TFactory>(services, serviceProvider),
            ServiceLifetime.Singleton));

        return services;
    }
}
