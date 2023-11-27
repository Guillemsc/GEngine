using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GEngine.Utils.Extensions
{
    public static class CloningExtensions
    {
        /// <summary>
        /// Creates a deep clone of an object.
        /// </summary>
        /// <typeparam name="T">The type of the object to clone.</typeparam>
        /// <param name="obj">The object to clone.</param>
        /// <returns>A deep clone of the object.</returns>
        public static T DeepClone<T>(this T obj)
        {
            using MemoryStream stream = new MemoryStream();

            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, obj);
            stream.Position = 0;

            return (T)formatter.Deserialize(stream);
        }
    }
}
