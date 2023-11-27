using System;

namespace GEngine.Utils.Observables.Variables
{
    /// <summary>
    /// Represents an observable variable of type T.
    /// </summary>
    /// <typeparam name="T">The type of the variable.</typeparam>
    public interface IObservableVariable<T>
    {
        /// <summary>
        /// Gets or sets the value of the variable.
        /// </summary>
        T Value { get; set; }

        /// <summary>
        /// Event that is triggered when the value of the variable changes.
        /// </summary>
        event Action<T> OnChange;
    }
}
