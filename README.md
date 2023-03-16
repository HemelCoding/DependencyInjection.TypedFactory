# Hemel.DependencyInjection.TypedFactory

A typed factory facility for Microsoft Extensions DependencyInjection.
This facility provides auto-implementations of factories based on the components registered to the service collection.

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
