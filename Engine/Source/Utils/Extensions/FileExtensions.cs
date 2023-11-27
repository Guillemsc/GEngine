using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using GEngine.Utils.DiscriminatedUnions;
using GEngine.Utils.Optionals;
using GEngine.Utils.Types;
using GEngine.Utils.Logging.Enums;
using GEngine.Utils.Logging.Loggers;

namespace GEngine.Utils.Extensions
{
    public static class FileExtensions
    {
        /// <summary>
        /// Creates directories for a file and deletes the file if it already exists.
        /// </summary>
        /// <param name="filePath">The path of the file.</param>
        /// <returns>
        /// An <see cref="Optional{T}"/> containing an <see cref="ErrorMessage"/> if an error occurs;
        /// otherwise, it contains <see cref="Optional{T}.None"/>.
        /// </returns>
        public static Optional<ErrorMessage> CreateFileDirectoriesAndDeleteFileIfExistsOrError(string filePath)
        {
            Optional<ErrorMessage> Run()
            {
                bool fileAlreadyExists = File.Exists(filePath);

                if (fileAlreadyExists)
                {
                    File.Delete(filePath);
                    return Optional<ErrorMessage>.None;
                }

                string filePathDirectory = Path.GetDirectoryName(filePath);

                if (string.IsNullOrEmpty(filePathDirectory))
                {
                    return new ErrorMessage("Provided filepath was invalid or file could not be found");
                }

                Directory.CreateDirectory(filePathDirectory);
                return Optional<ErrorMessage>.None;
            }

            return TryCatchExtensions.InvokeCatchError(Run);
        }

        /// <summary>
        /// Saves a byte array to a file asynchronously, with error handling.
        /// If the directory could not be found, it creates it.
        /// If file already existed, it overrides it.
        /// </summary>
        /// <param name="filePath">The path of the file to save the byte array to.</param>
        /// <param name="data">The byte array to be saved.</param>
        /// <param name="cancellationToken">The cancellation token to cancel the asynchronous operation.</param>
        /// <returns>
        /// A <see cref="Task{TResult}"/> representing the asynchronous operation. The task result is an <see cref="Optional{T}"/>
        /// containing an <see cref="ErrorMessage"/> if an error occurs; otherwise, it contains <see cref="Optional{T}.None"/>.
        /// </returns>
        public static Task<Optional<ErrorMessage>> SaveBytesWithErrorAsync(
            string filePath,
            byte[] data,
            CancellationToken cancellationToken
            )
        {
            Optional<ErrorMessage> optionalError = CreateFileDirectoriesAndDeleteFileIfExistsOrError(filePath);

            if (optionalError.HasValue)
            {
                return Task.FromResult(optionalError);
            }

            async Task Save(CancellationToken saveCancellationToken)
            {
                await using FileStream sourceStream = File.Open(filePath, FileMode.OpenOrCreate);
                sourceStream.Seek(0, SeekOrigin.End);

                await sourceStream.WriteAsync(data, 0, data.Length, saveCancellationToken);

                await sourceStream.DisposeAsync();
            }

            return TryCatchExtensions.InvokeCatchErrorAsync(Save, cancellationToken);
        }

        /// <summary>
        /// Saves an array of bytes to some directory asynchronously.
        /// If the directory could not be found, it creates it.
        /// If file already existed, it deletes it.
        /// </summary>
        public static async Task SaveBytesAsync(string filePath, byte[] data, CancellationToken cancellationToken)
        {
            Optional<ErrorMessage> optionalErrorMessage = await SaveBytesWithErrorAsync(filePath, data, cancellationToken);

            bool hasErrorMessage = optionalErrorMessage.TryGet(out ErrorMessage errorMessage);

            if (hasErrorMessage)
            {
                // DebugOnlyUnityLogger.Instance.Log(
                //     LogType.Error,
                //     "There was an error trying to save bytes async to filePath {0}, at {1). Error: {2}}",
                //     filePath,
                //     nameof(FileExtensions),
                //     errorMessage
                // );
            }
        }

        /// <summary>
        /// Loads bytes from a file asynchronously, along with an error message if any error occurs.
        /// </summary>
        /// <param name="filePath">The path of the file to load.</param>
        /// <param name="cancellationToken">The cancellation token to cancel the asynchronous operation.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains either the loaded bytes if the file exists,
        /// or an error message if the file does not exist or any error occurs during the loading process.
        /// </returns>
        public static Task<OneOf<byte[], ErrorMessage>> LoadBytesWithErrorAsync(
            string filePath,
            CancellationToken cancellationToken
        )
        {
            bool fileExists = File.Exists(filePath);

            if (!fileExists)
            {
                OneOf<byte[], ErrorMessage> error = new ErrorMessage("File does not exist");
                return Task.FromResult(error);
            }

            async Task<byte[]> Load(CancellationToken loadCancellationToken)
            {
                await using FileStream fileStream = new FileStream(filePath, FileMode.Open);

                byte[] loadedBytes = new byte[fileStream.Length];

                var _ = await fileStream.ReadAsync(loadedBytes, 0, (int)fileStream.Length, loadCancellationToken);

                await fileStream.DisposeAsync();

                return loadedBytes;
            }

            return TryCatchExtensions.InvokeGetResultOrCatchErrorAsync(
                Load,
                cancellationToken
            );
        }

        /// <summary>
        /// Loads the bytes from a file asynchronously.
        /// </summary>
        /// <param name="filePath">The path of the file to load.</param>
        /// <param name="cancellationToken">The cancellation token to cancel the asynchronous operation.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains an optional byte array if the file
        /// was successfully loaded,
        /// or <see cref="Optional{T}.None"/> if any error occurs during the loading process.
        /// </returns>
        public static async Task<Optional<byte[]>> LoadBytesAsync(
            string filePath,
            CancellationToken cancellationToken
        )
        {
            OneOf<byte[], ErrorMessage> oneOfResult = await LoadBytesWithErrorAsync(
                filePath,
                cancellationToken
            );

            bool hasError = oneOfResult.TryGetSecond(out ErrorMessage errorMessage);

            if (hasError)
            {
                // DebugOnlyUnityLogger.Instance.Log(
                //     LogType.Error,
                //     "There was an exception trying to load bytes async to filePath {0}, at {1). Exception: {2}}",
                //     filePath,
                //     nameof(FileExtensions),
                //     errorMessage
                // );
                return Optional<byte[]>.None;
            }

            return oneOfResult.UnsafeGetFirst();
        }
    }
}
