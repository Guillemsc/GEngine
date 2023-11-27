// using System.Text;
// using Popcore.Core.DiscriminatedUnions;
// using Popcore.Core.Extensions;
// using Popcore.Core.Optionals;
// using Popcore.Core.Persistence.StorageMethods;
// using Popcore.Core.Types;
//
// namespace Popcore.Core.Persistence.Methods
// {
//     /// <inheritdoc />
//     /// <summary>
//     /// Implementation of <see cref="IStorageMethod"/> where data gets saved as bytes to disk.
//     /// </summary>
//     public class FileStorageMethod : IStorageMethod
//     {
//         public Task<Optional<ErrorMessage>> Save(string localPath, string data, CancellationToken cancellationToken)
//         {
//             string finalPath = GetFilePathFromLocalPath(localPath);
//
//             byte[] dataBytes = Encoding.UTF8.GetBytes(data);
//
//             return FileExtensions.SaveBytesWithErrorAsync(finalPath, dataBytes, cancellationToken);
//         }
//
//         public async Task<OneOf<string, ErrorMessage>> Load(string localPath, CancellationToken cancellationToken)
//         {
//             string finalPath = GetFilePathFromLocalPath(localPath);
//
//             OneOf<byte[], ErrorMessage> optionalBytesResult = await FileExtensions.LoadBytesWithErrorAsync(
//                 finalPath,
//                 cancellationToken
//             );
//
//             bool hasResult = optionalBytesResult.TryGetSecond(out ErrorMessage errorMessage);
//
//             if (hasResult)
//             {
//                 return errorMessage;
//             }
//
//             byte[] bytes = optionalBytesResult.UnsafeGetFirst();
//
//             string finalString = Encoding.UTF8.GetString(bytes);
//
//             return finalString;
//         }
//
//         public static string GetFilePathFromLocalPath(string fileName)
//         {
//             return string.Format(
//                 "{0}{1}",
//                 GetStorageDirectory(),
//                 fileName
//             );
//         }
//
//         // public static string GetStorageDirectory()
//         // {
//         //     return Path.Combine(
//         //         Application.persistentDataPath,
//         //         $"SerializableData{Path.DirectorySeparatorChar}"
//         //     );
//         // }
//
//         public static void ClearAllStoredData()
//         {
//             string path = GetStorageDirectory();
//
//             Directory.Delete(path, recursive: true);
//         }
//
//         public static void ClearStoredData(string localPath)
//         {
//             string finalPath = GetFilePathFromLocalPath(localPath);
//
//             Directory.Delete(finalPath, recursive: true);
//         }
//     }
// }
