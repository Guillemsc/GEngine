using System.Diagnostics;
using GEngine.Utils.DiscriminatedUnions;
using GEngine.Utils.Optionals;
using GEngine.Utils.Persistence.Constants;
using GEngine.Utils.Persistence.Migrations;
using GEngine.Utils.Persistence.StorageMethods;
using GEngine.Utils.Types;
using Newtonsoft.Json;
using GEngine.Utils.Extensions;

namespace GEngine.Utils.Persistence.Serialization
{
    /// <inheritdoc />
    public sealed class SerializableData<T> : ISerializableData<T> where T : class
    {
        readonly IStorageMethod _storageMethod;
        readonly string _localPath;
        readonly IMigration<T>[] _migrations;

        T _data;

        public event Action<T> OnSave;
        public event Action<T, bool> OnLoad;

        public T Data
        {
            get => GetData();
            set => _data = value;
        }

        public SerializableData(IStorageMethod storageMethod, string localPath)
        {
            _storageMethod = storageMethod;
            _localPath = localPath;
            _migrations = Array.Empty<IMigration<T>>();
        }

        public SerializableData(IStorageMethod storageMethod, string localPath, params IMigration<T>[] migrations)
        {
            _storageMethod = storageMethod;
            _localPath = localPath;
            _migrations = migrations;
        }

        public async Task Save(CancellationToken cancellationToken)
        {
            TryGenerateData();

            OnSave?.Invoke(_data);

            try
            {
                Stopwatch saveStopWatch = Stopwatch.StartNew();

                string dataString = JsonConvert.SerializeObject(_data, GetSerializerSettings());

                Optional<ErrorMessage> optionalError = await _storageMethod.Save(_localPath, dataString, cancellationToken);

                saveStopWatch.Stop();

                bool hasOptionalError = optionalError.TryGet(out ErrorMessage errorMessage);

                if (hasOptionalError)
                {
                    // DebugOnlyUnityLogger.Instance.Log(
                    //     LogType.Info,
                    //     "{0} {1} could not be saved: {2}.",
                    //     nameof(SerializableData<T>),
                    //     typeof(T).Name,
                    //     errorMessage
                    // );
                    return;
                }

                // DebugOnlyUnityLogger.Instance.Log(
                //     LogType.Info,
                //     "{0} {1} saved ({2}ms) \n{3}",
                //     nameof(SerializableData<T>),
                //     LoggingStyledValue<string>.New(LoggingStyleConstants.VariableColor, typeof(T).Name),
                //     LoggingStyledValue<long>.New(LoggingStyleConstants.ValueColor, saveStopWatch.ElapsedMilliseconds),
                //     _data
                // );
            }
            catch (Exception exception)
            {
                // DebugOnlyUnityLogger.Instance.Log(
                //     LogType.Error,
                //     "Error saving {0} {1} with the following exception {2}",
                //     nameof(SerializableData<T>),
                //     typeof(T).Name,
                //     exception
                // );
            }
        }

        public async Task Load(CancellationToken cancellationToken)
        {
            try
            {
                Stopwatch loadStopWatch = Stopwatch.StartNew();

                OneOf<string, ErrorMessage> optionalLoadedData = await _storageMethod.Load(_localPath, cancellationToken);

                bool hasError = optionalLoadedData.TryGetSecond(out ErrorMessage errorMessage);

                if (hasError)
                {
                    TryGenerateData();

                    OnLoad?.Invoke(_data, /*First time:*/ true);

                    // DebugOnlyUnityLogger.Instance.Log(
                    //     LogType.Info,
                    //     "{0} {1} could not be loaded: {2}. Creating with default values",
                    //     nameof(SerializableData<T>),
                    //     typeof(T).Name,
                    //     errorMessage
                    // );
                }
                else
                {
                    string dataString = optionalLoadedData.UnsafeGetFirst();

                    _data = JsonConvert.DeserializeObject<T>(dataString, GetSerializerSettings());

                    foreach (IMigration<T> migration in _migrations)
                    {
                        migration.Migrate(_data);
                    }

                    OnLoad?.Invoke(_data, /*First time:*/ false);

                    // DebugOnlyUnityLogger.Instance.Log(
                    //     LogType.Info,
                    //     "{0} {1} loaded ({2}ms) \n{3}",
                    //     nameof(SerializableData<T>),
                    //     LoggingStyledValue<string>.New(LoggingStyleConstants.VariableColor, typeof(T).Name),
                    //     LoggingStyledValue<long>.New(LoggingStyleConstants.ValueColor, loadStopWatch.ElapsedMilliseconds),
                    //     Data
                    // );
                }

                loadStopWatch.Stop();
            }
            catch (Exception exception)
            {
                // DebugOnlyUnityLogger.Instance.Log(
                //     LogType.Error,
                //     "Error loading {0} {1} with the following exception {2}",
                //     nameof(SerializableData<T>),
                //     typeof(T).Name,
                //     exception
                // );
            }

            TryGenerateData();
        }

        public void SaveAsync()
        {
            Save(CancellationToken.None).RunAsync();
        }

        public void LoadAsync()
        {
            Load(CancellationToken.None).RunAsync();
        }

        T GetData()
        {
            if (_data == null)
            {
                TryGenerateData();

                //UnityEngine.Debug.LogError($"Tried to get Data before it was loaded, at {nameof(SerializableData<T>)} {typeof(T).Name}");
            }

            return _data;
        }

        void TryGenerateData()
        {
            if (_data != null)
            {
                return;
            }

            _data = Activator.CreateInstance<T>();
        }

        JsonSerializerSettings GetSerializerSettings()
        {
            return PersistenceConstants.DebugJsonSettings;
        }
    }
}
