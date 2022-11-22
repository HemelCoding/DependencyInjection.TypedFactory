using Hemel.DependencyInjection.TypedFactory.Tests.TestTypes;
using Microsoft.Extensions.DependencyInjection;

namespace Hemel.DependencyInjection.TypedFactory.Tests;

public class ServiceCollectionExTests
{
    [Fact]
    public void Works()
    {
        var services = new ServiceCollection();
        services.AddTransient<IService, Component>();
        services.AddTransient<IOtherService, OtherComponent>();
        services.AddTypedFactory<IFactory>();

        var provider = services.BuildServiceProvider();
        var factory = provider.GetRequiredService<IFactory>();
        var service = factory.CreateService(65);
        var result = service.DoSomething(76);

        Assert.Equal("65-76-65", result);
    }

    [Fact]
    public void TransientIsRespected()
    {
        var services = new ServiceCollection();
        services.AddTransient<IService, Component>();
        services.AddTransient<IOtherService, OtherComponent>();
        services.AddTypedFactory<IFactory>();

        var provider = services.BuildServiceProvider();
        var factory = provider.GetRequiredService<IFactory>();
        var service1 = factory.CreateService(65);
        var service2 = factory.CreateService(65);

        Assert.NotSame(service2, service1);
    }
}
