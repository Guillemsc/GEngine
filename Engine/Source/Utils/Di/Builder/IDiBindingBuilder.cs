using System;
using GEngine.Utils.Di.Container;

namespace GEngine.Utils.Di.Builder
{
    /// <summary>
    /// Builder that, for some type, helps define how that object will be created.
    /// </summary>
    public interface IDiBindingBuilder<T>
    {
        /// <summary>
        /// The object will be created using the default/empty constructor.
        /// </summary>
        /// <exception cref="System.Exception">Thrown when type does not have a default/empty constructor, or it's abstarct.</exception>
        /// <example>
        /// <code>
        /// builder.Bind"SomeClass"().FromNew());
        /// </code>
        /// </example>
        IDiBindingActionBuilder<T> FromNew();

        /// <summary>
        /// You need to provide the instance of the object directly here.
        /// </summary>
        /// <exception cref="System.Exception">Thrown when provided instance is null.</exception>
        /// <example>
        /// <code>
        /// builder.Bind"SomeClass"().FromInstance(new SomeClass()));
        /// </code>
        /// </example>
        IDiBindingActionBuilder<T> FromInstance(T instance);

        /// <summary>
        /// You need to provide a function that will be called when you try to resolve this object.
        /// This way of creating the object allows you to resolve dependencies using other objects
        /// from the container, by using the provided <see cref="IDiResolveContainer"/>.
        /// Be carefull with circular dependencies.
        /// </summary>
        /// <example>
        /// <code>
        /// builder.Bind"SomeClass"()
        ///     .FromFunction(c => new SomeClass(
        ///         c.Resolve"SomeDependency"()
        ///     ));
        /// </code>
        /// </example>
        IDiBindingActionBuilder<T> FromFunction(Func<IDiResolveContainer, T> func);

        /// <summary>
        /// You need to provide a function that will be called when you try to resolve this object.
        /// Be carefull with circular dependencies.
        /// </summary>
        /// <example>
        /// <code>
        /// builder.Bind"SomeClass"().FromFunction(c => new SomeClass());
        /// </code>
        /// </example>
        IDiBindingActionBuilder<T> FromFunction(Func<T> func);

        /// <summary>
        /// Instance will be retrieved from an existing binding on the container.
        /// This is usefull for binding different types with the same instance.
        /// </summary>
        /// <example>
        /// <code>
        /// public class SomeClass : IA, IB
        /// {
        /// }
        ///
        /// b.Bind"SomethingCool"()...
        /// b.Bind"IA, SomeClass"().FromContainer();
        /// b.Bind"IB, SomeClass"().FromContainer();
        /// </code>
        /// </example>
        IDiBindingActionBuilder<T> FromContainer();

        /// <summary>
        /// Instance will be retrieved from an external <see cref="IDiResolveContainer"/>.
        /// </summary>
        /// <example>
        /// <code>
        /// builder.Bind"SomeClass"().FromContainer(someOtherContainer));
        /// </code>
        /// </example>
        IDiBindingActionBuilder<T> FromContainer(IDiResolveContainer container);
    }
}
