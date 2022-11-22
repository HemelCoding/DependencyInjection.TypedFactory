namespace Hemel.DependencyInjection.TypedFactory.Tests.TestTypes;

public class OtherComponent : IOtherService
{
    public int GetValue()
    {
        return 65;
    }
}