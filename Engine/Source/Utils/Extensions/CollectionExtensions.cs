using System;
using System.Collections;

namespace GEngine.Utils.Extensions
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// From a type that's <see cref="ICollection"/>, tries to get the generic argument T.
        /// For example, from a List of ints, gets the int type.
        /// </summary>
        public static bool TryGetCollectionArgumentType(Type collectionType, out Type argumentType)
        {
            bool isCollectionChild = typeof(ICollection).IsAssignableFrom(collectionType);

            if (!isCollectionChild)
            {
                argumentType = default;
                return false;
            }

            if (!collectionType.IsGenericType)
            {
                argumentType = default;
                return false;
            }

            Type[] genericArguments = collectionType.GetGenericArguments();

            if (genericArguments.Length == 0)
            {
                argumentType = default;
                return false;
            }

            argumentType = genericArguments[0];
            return true;
        }
    }
}
