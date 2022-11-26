# Hemel.DependencyInjection.TypedFactory

This Hemel.DependencyInjection.TypedFactory package adds an extension method `AddTypedFactory<>` to the type `Microsoft.Extensions.DependencyInjection.IServiceCollection`.

This extension method will implement and register to the service collection a typed factory that is able to instantiate components previously registered to that service collection.
The typed factory interface is given to the `AddTypedFactory<>` method.

Parameters that are not given to the creation methods of the factory interface will be resolved from the service collection, allowing to inject services without having to explictly declare them to the creation method.
