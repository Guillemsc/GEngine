using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GEngine.Utils.Extensions
{
    public static class TypeExtensions
    {
        public static TNew HardCast<TNew>(this object obj)
        {
            return (TNew)Convert.ChangeType(obj, typeof(TNew))!;
        }

        /// <summary>
        /// Searches for the specified field, using the specified binding constraints.
        /// </summary>
        /// <param name="type">The Type to search for the field.</param>
        /// <param name="name">The name of the field to retrieve.</param>
        /// <param name="bindingFlags">A bitwise combination of BindingFlags that specifies how the search for the field is conducted.</param>
        /// <param name="fieldInfo">When found, contains the FieldInfo object representing the specified field.</param>
        /// <returns><c>true</c> if the field is found; otherwise, <c>false</c>.</returns>
        public static bool TryGetField(this Type type, string name, BindingFlags bindingFlags, out FieldInfo fieldInfo)
        {
            fieldInfo = type.GetField(name, bindingFlags);

            return fieldInfo != null;
        }

        /// <summary>
        /// Tries to retrieve the first generic argument from the specified <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The type to extract the generic argument from.</param>
        /// <param name="genericArgument">The first generic argument of the type, if found; otherwise, <c>null</c>.</param>
        /// <returns><c>true</c> if the first generic argument was successfully retrieved; otherwise, <c>false</c>.</returns>
        public static bool TryGetFirstGenericArgument(this Type type, out Type genericArgument)
        {
            return type.GenericTypeArguments.TryGet(0, out genericArgument);
        }

        /// <summary>
        /// Tries to retrieve a custom attribute of type <typeparamref name="T"/> from the specified <paramref name="type"/>.
        /// </summary>
        /// <typeparam name="T">The type of the custom attribute to retrieve.</typeparam>
        /// <param name="type">The type to retrieve the custom attribute from.</param>
        /// <param name="attribute">The custom attribute instance, if found; otherwise, <c>null</c>.</param>
        /// <returns><c>true</c> if the custom attribute was successfully retrieved; otherwise, <c>false</c>.</returns>
        public static bool TryGetFirstCustomAttribute<T>(this Type type, out T attribute) where T : Attribute
        {
            IEnumerable<T> attributes = type.GetCustomAttributes<T>();
            attribute = attributes.FirstOrDefault();

            return attribute != null;
        }

        /// <summary>
        /// Tries to retrieve a custom attribute from the specified <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The type to retrieve the custom attribute from.</param>
        /// <param name="attributeType">Type of the attribute to look for.</param>
        /// <param name="attribute">The custom attribute instance, if found; otherwise, <c>null</c>.</param>
        /// <returns><c>true</c> if the custom attribute was successfully retrieved; otherwise, <c>false</c>.</returns>
        public static bool TryGetFirstCustomAttribute(this Type type, Type attributeType, out Attribute attribute)
        {
            IEnumerable<Attribute> attributes = type.GetCustomAttributes(attributeType);
            attribute = attributes.FirstOrDefault();

            return attribute != null;
        }

        /// <summary>
        /// Retrieves a list of all methods in the specified <paramref name="type"/> that
        /// are decorated with the specified attribute <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of the attribute to search for.</typeparam>
        /// <param name="type">The type to search for methods with the specified attribute.</param>
        /// <returns>A list of <see cref="MethodInfo"/> instances representing the methods that have
        /// the specified attribute.</returns>
        public static List<MethodInfo> GetAllMethodsWithAttribute<T>(this Type type) where T : Attribute
        {
            List<MethodInfo> ret = new();

            MethodInfo[] methods = type.GetMethods();

            foreach (MethodInfo method in methods)
            {
                bool attributeFound = method.HasCustomAttribute<T>();

                if (attributeFound)
                {
                    ret.Add(method);
                }
            }

            return ret;
        }

        public static List<Tuple<MethodInfo, T>> GetAllMethodsAndAttributesWithAttribute<T>(this Type type) where T : Attribute
        {
            List<Tuple<MethodInfo, T>> ret = new();

            MethodInfo[] methods = type.GetMethods();

            foreach (MethodInfo method in methods)
            {
                bool attributeFound = method.TryGetCustomAttribute(out T attribute);

                if (attributeFound)
                {
                    ret.Add(new Tuple<MethodInfo, T>(method, attribute));
                }
            }

            return ret;
        }
    }
}
