# Hemel.DependencyInjection.TypedFactory

Let's say we want to create instances after an object's initialization, using a factory:

```csharp
public class MyObject: IMyObject {
    private readonly IFactory factory;
    private readonly List<ISubObject> subObjects = new();

    public MyObject(IFactory factory) {
        this.factory = factory;
    }

    public void CreateSubObject() {
        subObjects.Add(factory.Create());
    }
}
```

## Want to use dependency injection, but don't want to implement the factory?

```csharp
var services = new ServiceCollection();
services.AddTransient<IMyObject, MyObject>();
services.AddTransient<ISubObject, SubObject>();
services.AddTypedFactory<IFactory>(); // from Hemel.DependencyInjection.TypedFactory
var provider = services.BuildServiceProvider();

var myObject = provider.GetRequiredService<IMyObject>();
myObject.CreateSubObject();
```

## Want to add parameters to be forwarded to the constructor of the created object?

```diff
public class SubObject {
-    public SubObject() { /* ... */ }
+    public SubObject(Guid id) { /* ... */ }
}

public interface IFactory {
-    ISubObject Create();
+    ISubObject Create(Guid id);
}
```

```diff
var services = new ServiceCollection();
services.AddTransient<IMyObject, MyObject>();
services.AddTransient<ISubObject, SubObject>();
services.AddTypedFactory<IFactory>(); // from Hemel.DependencyInjection.TypedFactory
var provider = services.BuildServiceProvider();

var myObject = provider.GetRequiredService<IMyObject>();
- myObject.CreateSubObject();
+ myObject.CreateSubObject(Guid.NewGuid());
```

## Want to inject services into the instances created with the factory?

```diff
public class SubObject {
-    public SubObject(Guid id) { /* ... */ }
+    public SubObject(Guid id, ILogger logger) { /* ... */ }
}
```

```diff
var services = new ServiceCollection();
services.AddTransient<IMyObject, MyObject>();
services.AddTransient<ISubObject, SubObject>();
+ services.AddTransient<ILogger, Logger>();
services.AddTypedFactory<IFactory>(); // from Hemel.DependencyInjection.TypedFactory
var provider = services.BuildServiceProvider();

var myObject = provider.GetRequiredService<IMyObject>();
myObject.CreateSubObject();
```
