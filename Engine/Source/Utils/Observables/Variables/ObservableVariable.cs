using System;

namespace GEngine.Utils.Observables.Variables
{
    /// <inheritdoc />
    public sealed class ObservableVariable<T> : IObservableVariable<T>
    {
        T _value;

        public T Value
        {
            get => _value;

            set
            {
                if (_value.Equals(value))
                {
                    return;
                }

                _value = value;

                OnChange?.Invoke(value);
            }
        }

        public event Action<T> OnChange;
    }
}
