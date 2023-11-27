using GEngine.Utils.DiscriminatedUnions;
using GEngine.Utils.Extensions;
using GEngine.Utils.Types;
using Newtonsoft.Json;

namespace GEngine.Utils.Json.Extensions
{
    public static class JsonExtensions
    {
        // This is the default settings we'll use. Cached here because newtonsoft is super slow when changing these.
        static readonly JsonSerializerSettings JsonDeserializeSettings = new()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Formatting = Formatting.None,
        };

        /// <summary>
        /// Deserializes the specified JSON string into an object of the specified type, wrapped in an instance of the OneOf type.
        /// If an exception occurs during deserialization, an ErrorMessage containing the exception message is returned.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize.</typeparam>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>An instance of OneOf that encapsulates either the deserialized object or an ErrorMessage if an exception occurs.</returns>
        public static OneOf<T, ErrorMessage> Deserialize<T>(string json)
        {
            return TryCatchExtensions.InvokeGetResultOrCatchError(
                x => JsonConvert.DeserializeObject<T>(x, JsonDeserializeSettings),
                json
            );
        }

        /// <summary>
        /// Serializes the specified object into a JSON string, wrapped in an instance of the OneOf type.
        /// If an exception occurs during serialization, an ErrorMessage containing the exception message is returned.
        /// </summary>
        /// <typeparam name="T">The type of the object to serialize.</typeparam>
        /// <param name="obj">The object to serialize.</param>
        /// <returns>An instance of OneOf that encapsulates either the serialized JSON string or an ErrorMessage if an exception occurs.</returns>
        public static OneOf<string, ErrorMessage> Serialize<T>(T obj)
        {
            return TryCatchExtensions.InvokeGetResultOrCatchError(
                x => JsonConvert.SerializeObject(x, JsonDeserializeSettings),
                obj
            );
        }

        /// <summary>
        /// Tries to deserialize the specified JSON string into an object of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize.</typeparam>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <param name="data">When this method returns, contains the deserialized object if the deserialization succeeds,
        /// or the default value of <typeparamref name="T"/> if the deserialization fails.</param>
        /// <returns><c>true</c> if the deserialization succeeds; otherwise, <c>false</c>.</returns>
        public static bool TryDeserialize<T>(string json, out T data)
        {
            OneOf<T, ErrorMessage> result = Deserialize<T>(json);

            return result.TryGetFirst(out data);
        }

        /// <summary>
        /// Tries to serialize the specified object into a JSON string.
        /// </summary>
        /// <typeparam name="T">The type of the object to serialize.</typeparam>
        /// <param name="obj">The object to serialize.</param>
        /// <param name="json">When this method returns, contains the serialized JSON string if the serialization succeeds;
        /// otherwise, an empty string.</param>
        /// <returns><c>true</c> if the serialization succeeds; otherwise, <c>false</c>.</returns>
        public static bool TrySerialize<T>(T obj, out string json)
        {
            OneOf<string, ErrorMessage> result = Serialize(obj);

            return result.TryGetFirst(out json);
        }

        public static string AddEscapeCharacters(string json)
        {
            return json.Replace(@"""", @"\""");
        }

        public static string RemoveEscapeCharacters(string json)
        {
            return json.Replace(@"\""", @"""");
        }
    }
}
