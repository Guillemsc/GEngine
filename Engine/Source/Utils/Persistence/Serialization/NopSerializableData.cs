using System;
using System.Threading;
using System.Threading.Tasks;

namespace GEngine.Utils.Persistence.Serialization
{
    public sealed class NopSerializableData<T> : ISerializableData<T> where T : class
    {
        public static readonly NopSerializableData<T> Instance = new();

#pragma warning disable 67
        public event Action<T> OnSave;
        public event Action<T, bool> OnLoad;
#pragma warning restore 67

        public T Data { get; set; }

        public NopSerializableData()
        {
            Data = Activator.CreateInstance<T>();
        }

        public NopSerializableData(T data)
        {
            Data = data;
        }

        public Task Save(CancellationToken cancellationToken) => Task.CompletedTask;
        public Task Load(CancellationToken cancellationToken) => Task.CompletedTask;
        public void SaveAsync() { }
        public void LoadAsync() { }
    }
}
