using GEngine.Utils.Di.Container;
using GEngine.Utils.Di.Delegates;
using GEngine.Utils.Di.Installers;

namespace GEngine.Utils.Di.Builder
{
    /// <summary>
    /// Builder for setting up a <see cref="IDiContainer"/>.
    /// Bind and Install all necessary values, and then call Build to generate the final <see cref="IDiContainer"/>.
    /// </summary>
    public interface IDiContainerBuilder
    {
        /// <summary>
        /// Adds some settings that can be read at installation time.
        /// This is useful for dynamically modify what gets installed depending on parameters.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">Thrown when settings type has already been added.</exception>
        void AddSettings<T>(T settings);

        /// <summary>
        /// Gets some settings that can be read at installation time.
        /// This is useful for dynamically modify what gets installed depending on parameters.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">Thrown when settings type cannot be found.</exception>
        T GetSettings<T>();

        /// <summary>
        /// Gets some settings that can be read at installation time.
        /// This is useful for dynamically modify what gets installed depending on parameters.
        /// </summary>
        bool TryGetSettings<T>(out T settings);

        /// <summary>
        /// Begins the binding/registering of some type.
        /// Only one instance of the same type can be binded to the same container.
        /// </summary>
        IDiBindingBuilder<T> Bind<T>();

        IDiBindingBuilder<T> Bind<T>(object id);

        /// <summary>
        /// Begins the binding of some type, where the TInterface acts as the identifier type, and TConcrete
        /// acts as the type that's going to be used for setting up the binding.
        /// Only one instance of the same type can be binded to the same container.
        /// </summary>
        IDiBindingBuilder<TConcrete> Bind<TInterface, TConcrete>() where TConcrete : TInterface;

        IDiBindingBuilder<TConcrete> Bind<TInterface, TConcrete>(object id) where TConcrete : TInterface;

        /// <summary>
        /// Binds some type using a delegate.
        /// Only one instance of the same type can be binded to the same container.
        /// </summary>
        IDiContainerBuilder Bind<T>(BindingBuilderDelegate<T> bindingBuilderDelegate);


        [Obsolete("Use Install instead")]
        IDiContainerBuilder Bind(IEnumerable<IInstaller> installers);

        [Obsolete("Use Install instead")]
        IDiContainerBuilder Bind(params IInstaller[] installers);

        [Obsolete("Use Install instead")]
        IDiContainerBuilder Bind(IReadOnlyList<IInstaller> installers);

        [Obsolete("Use Install instead")]
        IDiContainerBuilder Bind(Action<IDiContainerBuilder> action);

        /// <summary>
        /// Installs all the bindings from a list of <see cref="IInstaller"/>s.
        /// </summary>
        IDiContainerBuilder Install(IEnumerable<IInstaller> installers);

        /// <summary>
        /// Installs all the bindings from a list of <see cref="IInstaller"/>s.
        /// </summary>
        IDiContainerBuilder Install(params IInstaller[] installers);

        /// <summary>
        /// Installs all the bindings from an <see cref="InstallDelegate"/>.
        /// </summary>
        IDiContainerBuilder Install(InstallDelegate installDelegate);

        /// <summary>
        /// Installs all the bindings from a list of <see cref="IInstaller"/>s.
        /// </summary>
        IDiContainerBuilder Install(IReadOnlyList<IInstaller> installers);

        /// <summary>
        /// Invokes the action with the current container builder.
        /// Usefull for installing through a method.
        /// </summary>
        IDiContainerBuilder Install(Action<IDiContainerBuilder> action);

        /// <summary>
        /// Tries to get a binding from the builder.
        /// </summary>
        bool TryGetBinding<T>(out IDiBindingActionBuilder<T> diBindingActionBuilder);

        /// <summary>
        /// Calls the action just after the <see cref="IDiContainer"/> gets builded.
        /// </summary>
        IDiContainerBuilder WhenBuild(Action<IDiContainer?> action);

        /// <summary>
        /// Calls the action after the <see cref="IDiContainer"/> gets builded, and before non lazy bindings get binded.
        /// </summary>
        IDiContainerBuilder WhenInit(Action action);

        /// <summary>
        /// Calls the action after the <see cref="IDiContainer"/> gets builded, and before non lazy bindings get binded.
        /// </summary>
        IDiContainerBuilder WhenInit(Action<IDiResolveContainer> action);

        /// <summary>
        /// Calls the action when the builded <see cref="IDiContainer"/> gets disposed.
        /// </summary>
        IDiContainerBuilder WhenDispose(Action action);

        /// <summary>
        /// Calls the action when the builded <see cref="IDiContainer"/> gets disposed.
        /// </summary>
        IDiContainerBuilder WhenDispose(Action<IDiResolveContainer> action);
        
        /// <summary>
        /// Generates the <see cref="IDiContainer"/> from the current builder.
        /// At this point all non lazy bindings will get binded.
        /// </summary>
        IDiContainer? Build();

        /// <summary>
        /// Generates the <see cref="IDiContainer"/> from the current builder, and an extra container.
        /// At this point all non lazy bindings will get binded.
        /// </summary>
        IDiContainer? Build(IDiContainer parentContainer);

        /// <summary>
        /// Generates the <see cref="IDiContainer"/> from the current builder, and some extra containers.
        /// At this point all non lazy bindings will get binded.
        /// </summary>
        IDiContainer? Build(IReadOnlyList<IDiContainer> extraContainers);
    }
}
