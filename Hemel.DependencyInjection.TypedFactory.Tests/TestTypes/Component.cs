namespace Hemel.DependencyInjection.TypedFactory.Tests.TestTypes;

public class Component : IService
{
    private readonly IOtherService otherService;
    private readonly int value;

    public Component(IOtherService otherService, int value)
    {
        this.otherService = otherService;
        this.value = value;
    }

    public string DoSomething(int value)
    {
        return $"{otherService.GetValue()}-{value}-{this.value}";
    }
}