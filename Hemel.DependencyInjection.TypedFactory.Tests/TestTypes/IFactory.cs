namespace Hemel.DependencyInjection.TypedFactory.Tests.TestTypes;

public interface IFactory
{
    IService CreateService(int value);
}