using Hemel.DependencyInjection.TypedFactory;
using Microsoft.Extensions.DependencyInjection;

namespace Sample;

internal static class ServiceCollectionEx
{
    public static IServiceCollection AddVms(this IServiceCollection services)
    {
        services.AddSingleton<ILogger, Logger>();
        services.AddTransient<IItemVm, ItemVm>();
        services.AddTypedFactory<IItemFactory>();
        services.AddTransient<IMainVm, MainVm>();

        return services;
    }
}
