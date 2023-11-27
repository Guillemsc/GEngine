using System;
using GEngine.Utils.Di.Container;

namespace GEngine.Utils.Di.Builder
{
    /// <summary>
    /// Builder that, for some binded type with an instance, helps define some extra actions.
    /// </summary>
    public interface IDiBindingActionBuilder<T>
    {
        /// <summary>
        /// Type used to resolve the instance.
        /// </summary>
        Type IdentifierType { get; }

        /// <summary>
        /// Real type used for setting up the binding.
        /// </summary>
        Type ActualType { get; }

        /// <summary>
        /// By default, bindings are only executed when they are Resolved.
        /// To change this behaviour, you can mark them as non lazy, and they will be executed
        /// right when the container is built.
        /// </summary>
        IDiBindingActionBuilder<T> NonLazy();

        /// <summary>
        /// This action will be called right after the binding gets resolved, and will provide the binded instance
        /// plus the <see cref="IDiResolveContainer"/>.
        /// </summary>
        IDiBindingActionBuilder<T> WhenInit(Action<IDiResolveContainer, T> action);

        /// <summary>
        /// This action will be called right after the binding gets resolved, and will provide the binded instance.
        /// </summary>
        IDiBindingActionBuilder<T> WhenInit(Action<T> action);

        /// <summary>
        /// This action will be called right after the binding gets resolved.
        /// </summary>
        IDiBindingActionBuilder<T> WhenInit(Action action);

        /// <summary>
        /// This action will be called right after the binding gets resolved, and will provide the binded instance.
        /// You need to return an action that will be called instantly.
        /// </summary>
        IDiBindingActionBuilder<T> WhenInit(Func<T, Action> func);

        /// <summary>
        /// This action will be called right before the container gets disposed, and will provide the binded instance
        /// plus the <see cref="IDiResolveContainer"/>.
        /// </summary>
        IDiBindingActionBuilder<T> WhenDispose(Action<IDiResolveContainer, T> action);

        /// <summary>
        /// This action will be called right before the container gets disposed, and will provide the binded instance.
        /// </summary>
        IDiBindingActionBuilder<T> WhenDispose(Action<T> action);

        /// <summary>
        /// This action will be called right before the container gets disposed.
        /// </summary>
        IDiBindingActionBuilder<T> WhenDispose(Action action);

        /// <summary>
        /// his action will be called right before the container gets dispose, and will provide the binded instance.
        /// You need to return an action that will be called instantly.
        /// </summary>
        IDiBindingActionBuilder<T> WhenDispose(Func<T, Action> func);

        /// <summary>
        /// This disposable will be disposed right before the container gets disposed.
        /// </summary>
        IDiBindingActionBuilder<T> WhenDispose(IDisposable disposable);
    }
}
