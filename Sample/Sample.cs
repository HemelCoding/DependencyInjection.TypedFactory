using Microsoft.Extensions.DependencyInjection;

namespace Sample;

public class Sample
{
    [Fact]
    public void Working()
    {
        var services = new ServiceCollection();
        services.AddVms();
        var provider = services.BuildServiceProvider();

        var mainVm = provider.GetRequiredService<IMainVm>();
        mainVm.NewItem();
        mainVm.NewItem();
        Assert.Equal(2, mainVm.Items.Count());
    }
}
