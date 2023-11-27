using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GEngine.Utils.Extensions
{
    public static class FieldInfoExtensions
    {
        /// <summary>
        /// Tries to retrieve a custom attribute of a specified type.
        /// </summary>
        public static bool TryGetFirstCustomAttribute<T>(this FieldInfo fieldInfo, out T attribute) where T : Attribute
        {
            IEnumerable<T> attributes = fieldInfo.GetCustomAttributes<T>();
            attribute = attributes.FirstOrDefault();

            return attribute != null;
        }

        /// <summary>
        /// Tries to retrieve a custom attribute of a specified type.
        /// </summary>
        public static bool TryGetFirstCustomAttribute(this FieldInfo fieldInfo, Type attributeType, out Attribute attribute)
        {
            IEnumerable<Attribute> attributes = fieldInfo.GetCustomAttributes(attributeType);
            attribute = attributes.FirstOrDefault();

            return attribute != null;
        }

        /// <summary>
        /// Tries to get the type from which the current <see cref="T:System.Type" /> directly inherits.
        /// </summary>
        /// <param name="fieldInfo">The <see cref="FieldInfo"/> object representing the field.</param>
        /// <param name="baseType">When this method returns, contains the base type of the field if found; otherwise, <c>null</c>.</param>
        /// <returns><c>true</c> if the base type of the field was found; otherwise, <c>false</c>.</returns>
        public static bool TryGetBaseType(this FieldInfo fieldInfo, out Type baseType)
        {
            baseType = fieldInfo.FieldType.BaseType;
            return fieldInfo.FieldType.BaseType != null;
        }
    }
}
