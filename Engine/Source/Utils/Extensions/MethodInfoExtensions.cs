using System;
using System.Reflection;

namespace GEngine.Utils.Extensions
{
    public static class MethodInfoExtensions
    {
        /// <summary>
        /// Using reflection, checks if some method has been overriden by an inherited type.
        /// </summary>
        public static bool IsOverriden(this MethodInfo methodInfo)
        {
            return methodInfo.GetBaseDefinition().DeclaringType != methodInfo.DeclaringType;
        }

        /// <summary>
        /// Using reflection, checks if some method has been overriden by an inherited type.
        /// </summary>
        public static bool TryGetCustomAttribute<T>(this MethodInfo methodInfo, out T attribute) where T : Attribute
        {
            attribute = methodInfo.GetCustomAttribute<T>();
            return attribute != null;
        }

        /// <summary>
        /// Using reflection, checks if some method has been overriden by an inherited type.
        /// </summary>
        public static bool HasCustomAttribute<T>(this MethodInfo methodInfo) where T : Attribute
        {
            return methodInfo.TryGetCustomAttribute<T>(out _);
        }

        /// <summary>
        /// Invokes the specified <paramref name="methodInfo"/> on the provided <paramref name="object"/>.
        /// </summary>
        /// <param name="methodInfo">The <see cref="MethodInfo"/> representing the method to invoke.</param>
        /// <param name="object">The object on which to invoke the method.</param>
        public static void Invoke(this MethodInfo methodInfo, object @object)
        {
            methodInfo.Invoke(@object, Array.Empty<object>());
        }
    }
}
