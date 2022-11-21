using System.Reflection;
using System.Reflection.Emit;
using Microsoft.Extensions.DependencyInjection;

namespace Hemel.DependencyInjection.TypedFactory;

internal static class TypedFactoryCreator
{
    public static TFactory Create<TFactory>(IServiceCollection services, IServiceProvider serviceProvider)
        where TFactory: notnull
    {
        var factoryType = typeof(TFactory);
        var assemblyName = "TypedFactory_Implementations";
        var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(assemblyName), AssemblyBuilderAccess.Run);
        var moduleBuilder = assemblyBuilder.DefineDynamicModule(assemblyName);
        var typeBuilder = moduleBuilder.DefineType($"TypedFactory__{factoryType.Name}__Implementation");
        typeBuilder.AddInterfaceImplementation(factoryType);

        var serviceProviderField = typeBuilder.DefineField("serviceProvider", typeof(IServiceProvider), FieldAttributes.Private);
        var ctor = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, new[] { typeof(IServiceProvider) });
        var ctorGen = ctor.GetILGenerator();
        ctorGen.Emit(OpCodes.Ldarg_0);
        ctorGen.Emit(OpCodes.Call, typeof(object).GetConstructor(BindingFlags.Public | BindingFlags.Instance, Type.EmptyTypes) ?? throw new Exception());
        ctorGen.Emit(OpCodes.Ldarg_0);
        ctorGen.Emit(OpCodes.Ldarg_1);
        ctorGen.Emit(OpCodes.Stfld, serviceProviderField);
        ctorGen.Emit(OpCodes.Ret);

        foreach (var method in factoryType.GetMethods().Where(x => x.Name.StartsWith("Create")))
        {
            var serviceDescriptor = services.FirstOrDefault(x => x.ServiceType == method.ReturnType);
            if (serviceDescriptor is null)
            {
                throw new Exception($"An implementation was not found for the service {method.ReturnType}");
            }

            if (serviceDescriptor.Lifetime is not ServiceLifetime.Transient)
            {
                throw new Exception($"The lifetime of service {method.ReturnType} is not transient.");
            }

            var componentType = serviceDescriptor.ImplementationType;
            if (componentType is null)
            {
                throw new Exception($"The service {method.ReflectedType} does not have an implementation type.");
            }

            ImplementCreateMethod(
                typeBuilder,
                method,
                componentType,
                serviceProviderField);
        }

        var type = typeBuilder.CreateType() ?? throw new Exception("unable to create type");

        return (TFactory)(Activator.CreateInstance(type, serviceProvider) ?? throw new Exception());
    }

    private static void ImplementCreateMethod(TypeBuilder typeBuilder, MethodInfo info, Type componentType, FieldBuilder serviceProviderField)
    {
        var parameters = info.GetParameters().Select(x => x.ParameterType).ToArray();
        var builder = typeBuilder.DefineMethod(info.Name, MethodAttributes.Public | MethodAttributes.Virtual, info.ReturnType, parameters);
        var gen = builder.GetILGenerator();

        var localArray = gen.DeclareLocal(typeof(object[]));

        // create and store all parameters in an array
        gen.Emit(OpCodes.Ldc_I4, parameters.Length);
        gen.Emit(OpCodes.Newarr, typeof(object));
        gen.Emit(OpCodes.Stloc, localArray); // stores the array instance
        for (int i = 0; i < parameters.Length; ++i)
        {
            gen.Emit(OpCodes.Ldloc, localArray); // array instance
            gen.Emit(OpCodes.Ldc_I4, i); // index in the array
            gen.Emit(OpCodes.Ldarg, i + 1); // object to store
            if (parameters[i].IsValueType)
            {
                gen.Emit(OpCodes.Box, parameters[i]);
            }
            gen.Emit(OpCodes.Stelem_Ref); // do store the object into the arry at the given index
        }

        // push serviceProvider to the stack
        gen.Emit(OpCodes.Ldarg_0);
        gen.Emit(OpCodes.Ldfld, serviceProviderField);

        // push componentType to the stack
        gen.Emit(OpCodes.Ldtoken, componentType);
        gen.Emit(OpCodes.Call, typeof(Type).GetMethod("GetTypeFromHandle") ?? throw new Exception("typeof not found?"));

        // push array
        gen.Emit(OpCodes.Ldloc, localArray);

        // call ActivatorUtilities.CreateInstance
        var method = typeof(ActivatorUtilities)
                .GetMethod(
                    "CreateInstance",
                    new[] { typeof(IServiceProvider), typeof(Type), typeof(object[]) })
                ?? throw new Exception("method ActivatorUtilities.CreateInstance not found");
        gen.Emit(OpCodes.Call, method);

        gen.Emit(OpCodes.Ret);
    }
}
