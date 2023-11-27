namespace GEngine.Utils.Extensions
{
    public static class DirectoryExtensions
    {
        /// <summary>
        /// Checks if a directory exists <see cref="Directory.Exists"/>.
        /// If it does, it deletes it <see cref="Directory.Delete(string, bool)"/>.
        /// </summary>
        /// <param name="path">The path of the directory to remove.</param>
        /// <param name="recursive">True to remove directories, subdirectories, and files in path; otherwise, false.</param>
        /// <returns>If the directory could be found for deletion.</returns>
        public static bool DeleteIfExists(string path, bool recursive)
        {
            if (!Directory.Exists(path))
            {
                return false;
            }

            Directory.Delete(path, recursive);

            return true;
        }

        /// <summary>
        /// Creates all directories and subdirectories in the specified path unless they already exist.
        /// Checks if a directory exists <see cref="Directory.Exists"/>.
        /// If it does not, it creates it <see cref="Directory.CreateDirectory(string)"/>.
        /// </summary>
        /// <param name="path">The path of the directory to create.</param>
        /// <returns>If the directory did not exist, thus it can be created.</returns>
        public static bool CreateDirectoryIfDoesNotExists(string path)
        {
            if (Directory.Exists(path))
            {
                return false;
            }

            Directory.CreateDirectory(path);

            return true;
        }
    }
}
