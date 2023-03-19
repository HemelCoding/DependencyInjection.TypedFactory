# Hemel.DependencyInjection.TypedFactory

This Hemel.DependencyInjection.TypedFactory package adds an extension method `AddTypedFactory<>` to the type `Microsoft.Extensions.DependencyInjection.IServiceCollection`.

This extension method will implement and register to the service collection a typed factory that is able to instantiate components previously registered to that service collection.
The typed factory interface is given to the `AddTypedFactory<>` method.

Parameters that are not given to the creation methods of the factory interface will be resolved from the service collection, allowing to inject services without having to explictly declare them to the creation method.

## Getting started

Here is the NuGet package: [![Nuget](https://img.shields.io/nuget/dt/Hemel.DependencyInjection.TypedFactory)](https://www.nuget.org/packages/Hemel.DependencyInjection.TypedFactory)

Install this package into your project then use the extension method `AddTypedFactory<>` for `ServiceCollection`.

```csharp
var services = new ServiceCollection();
services.AddTypedFactory<IFactory>();
// Register other components.

var provider = services.BuildServiceProvider();

var factory = provider.GetRequiredService<IFactory>();
```

## Usage

TODO
Examples about how to use your package by providing code snippets/example images, or samples links on GitHub if applicable. 

- Provide sample code using code snippets
- Include screenshots, diagrams, or other visual help users better understand how to use your package

## Additional documentation

Documentation: https://hemelcoding.github.io/DependencyInjection.TypedFactory/

## Feedback

If you find any issue with this package, please open an Issue on github: [Open an issue](https://github.com/HemelCoding/DependencyInjection.TypedFactory/issues/new)
