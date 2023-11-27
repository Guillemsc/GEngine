using System;
using System.Collections.Generic;
using System.Reflection;
using GEngine.Utils.Optionals;

namespace GEngine.Utils.Extensions
{
    public static class ReflectionExtensions
    {
        /// <summary>
        /// Returns a list of types that inherit from the specified base type.
        /// </summary>
        /// <param name="baseType">The base type to search for.</param>
        /// <param name="includeAbstractsAndInterfaces">Whether to include abstract classes and interfaces in the result.</param>
        /// <returns>A list of types that inherit from the specified base type.</returns>
        public static List<Type> GetInheritedTypes(Type baseType, bool includeAbstractsAndInterfaces = false)
        {
            List<Type> ret = new List<Type>();

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach(Assembly assembly in assemblies)
            {
                Type[] types = assembly.GetTypes();

                foreach(Type type in types)
                {
                    if (!baseType.IsAssignableFrom(type))
                    {
                        continue;
                    }

                    if (baseType == type)
                    {
                        continue;
                    }

                    bool isAbstractOrInterface = type.IsAbstract || type.IsInterface;

                    if(!includeAbstractsAndInterfaces && isAbstractOrInterface)
                    {
                        continue;
                    }

                    ret.Add(type);
                }
            }

            return ret;
        }

        /// <summary>
        /// Retrieves the parent type of a specified generic type definition from a given type.
        /// </summary>
        /// <param name="type">The type from which to begin the search for the generic parent type.</param>
        /// <param name="genericParentType">The generic type definition of the parent type to search for.</param>
        /// <returns>
        /// An <see cref="Optional{T}"/> object.
        /// If a generic parent type that matches the provided generic type definition is found,
        /// the <see cref="Optional{T}"/> object contains that type.
        /// If such a type is not found, the <see cref="Optional{T}"/> object contains no value (None).
        /// </returns>
        /// <remarks>
        /// This extension method retrieves the parent type that is a generic instantiation of a specified generic type definition.
        /// It starts from the specified type and moves up the inheritance hierarchy until it finds a matching type or exhausts all parent types.
        /// </remarks>
        public static Optional<Type> GetParentTypeOfGenericTypeDefinition(this Type type, Type genericParentType)
        {
            Type childType = type.BaseType;
            while (childType != null)
            {
                if (childType.IsGenericType && childType.GetGenericTypeDefinition() == genericParentType)
                {
                    return childType;
                }

                childType = childType.BaseType;
            }

            return Optional<Type>.None;
        }

        public static List<Type> GetAllTypesWithAttribute<T>() where T : Attribute
        {
            Type type = typeof(T);
            return GetAllTypesWithAttribute(type);
        }

        public static List<Type> GetAllTypesWithAttribute(Type attributeType)
        {
            List<Type> ret = new List<Type>();

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach(Assembly assembly in assemblies)
            {
                Type[] types = assembly.GetTypes();

                foreach(Type type in types)
                {
                    bool hasAttribute = type.TryGetFirstCustomAttribute(attributeType, out _);

                    if (hasAttribute)
                    {
                        ret.Add(type);
                    }
                }
            }

            return ret;
        }

        public static List<FieldInfo> GetAllFieldsWithAttribute<T>() where T : Attribute
        {
            Type type = typeof(T);
            return GetAllFieldsWithAttribute(type);
        }

        public static List<FieldInfo> GetAllFieldsWithAttribute(Type attributeType)
        {
            List<FieldInfo> ret = new List<FieldInfo>();

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach(Assembly assembly in assemblies)
            {
                Type[] types = assembly.GetTypes();

                foreach(Type type in types)
                {
                    FieldInfo[] fields = type.GetFields();

                    foreach (FieldInfo field in fields)
                    {
                        bool hasAttribute = field.TryGetFirstCustomAttribute(attributeType, out _);

                        if (hasAttribute)
                        {
                            ret.Add(field);
                        }
                    }
                }
            }

            return ret;
        }

        public static List<Type> GetAllTypesWithAttributeOnAnyField<T>() where T : Attribute
        {
            Type type = typeof(T);
            return GetAllTypesWithAttributeOnAnyField(type);
        }

        public static List<Type> GetAllTypesWithAttributeOnAnyField(Type attributeType)
        {
            List<Type> ret = new List<Type>();

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach(Assembly assembly in assemblies)
            {
                Type[] types = assembly.GetTypes();

                foreach(Type type in types)
                {
                    FieldInfo[] fields = type.GetFields();

                    foreach (FieldInfo field in fields)
                    {
                        bool hasAttribute = field.TryGetFirstCustomAttribute(attributeType, out _);

                        if (hasAttribute)
                        {
                            ret.Add(type);
                            break;
                        }
                    }
                }
            }

            return ret;
        }
    }
}
