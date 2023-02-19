## Example

```csharp
interface IService {
    
}
class Service: IService { /*...*/ }

interface IFactory {
    IService Create(string parameter);
}

static class Program {
    static void Main() {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddTransient<IService, Service>();
        serviceCollection.AddTypedFactory<IFactory>();

        var services = serviceCollection.BuildServiceProvider();

        var factory = services.Resolve<IFactory>();
        var service = factory.Create("a parameter");
    }
}
```
