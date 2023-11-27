#if UNITY_EDITOR
using System;
using System.Reflection;
using UnityEditor;

namespace Popcore.Core.Extensions
{
    public static class SerializedPropertyExtensions
    {
        /// <summary>
        /// Iterates through all visible children of the serialized property and invokes the given action on each one.
        /// </summary>
        /// <param name="serializedProperty">The serialized property to iterate over.</param>
        /// <param name="action">The action to invoke on each visible child of the serialized property.</param>
        public static void ForeachVisibleChildren(
            this SerializedProperty serializedProperty,
            Action<SerializedProperty> action
        )
        {
            if (!serializedProperty.hasVisibleChildren)
            {
                return;
            }

            SerializedProperty endProperty = serializedProperty.GetEndProperty();

            serializedProperty.NextVisible(true);

            while (!SerializedProperty.EqualContents(serializedProperty, endProperty))
            {
                action.Invoke(serializedProperty);
                serializedProperty.NextVisible(false);
            }
        }

        /// <summary>
        /// Gets the combined height of all visible children of the serialized property.
        /// </summary>
        /// <param name="serializedProperty">The serialized property to measure the height of its visible children.</param>
        /// <returns>The combined height of all visible children of the serialized property.</returns>
        public static float GetVisibleChildrenHeight(this SerializedProperty serializedProperty)
        {
            if (!serializedProperty.hasVisibleChildren)
            {
                return 0;
            }

            float height = 0f;
            SerializedProperty endProperty = serializedProperty.GetEndProperty();

            serializedProperty.NextVisible(true);

            while (!SerializedProperty.EqualContents(serializedProperty, endProperty))
            {
                height += EditorGUI.GetPropertyHeight(serializedProperty);
                serializedProperty.NextVisible(false);
                height += EditorGUIUtility.standardVerticalSpacing;
            }

            return height;
        }

        /// <summary>
        /// Adds an element to the end of the array serialized property and assigns the given Unity object to it.
        /// </summary>
        /// <typeparam name="T">The type of Unity object to assign to the new array element.</typeparam>
        /// <param name="serializedProperty">The serialized property representing the array to add the element to.</param>
        /// <param name="unityObject">The Unity object to assign to the new array element.</param>
        /// <exception cref="Exception">Thrown if the serialized property is not an array.</exception>
        public static void AddElementToArray<T>(this SerializedProperty serializedProperty, T unityObject) where  T : UnityEngine.Object
        {
            if(!serializedProperty.isArray)
            {
                throw new Exception("SerializedProperty is not of type array");
            }

            serializedProperty.arraySize++;

            SerializedProperty elementProperty = serializedProperty.GetArrayElementAtIndex(serializedProperty.arraySize - 1);

            elementProperty.objectReferenceValue = unityObject;
        }

        /// <summary>
        /// Removes the array element at the given index from the serialized property.
        /// </summary>
        /// <param name="serializedProperty">The serialized property representing the array to remove the element from.</param>
        /// <param name="index">The index of the array element to remove.</param>
        /// <exception cref="Exception">Thrown if the serialized property is not an array.</exception>
        public static void RemoveElementFromArray(
            this SerializedProperty serializedProperty,
            int index
        )
        {
            if(!serializedProperty.isArray)
            {
                throw new Exception("SerializedProperty is not of type array");
            }

            if(serializedProperty.arraySize <= index)
            {
                return;
            }

            int oldSize = serializedProperty.arraySize;

            serializedProperty.DeleteArrayElementAtIndex(index);

            if (serializedProperty.arraySize == oldSize)
            {
                serializedProperty.DeleteArrayElementAtIndex(index);
            }
        }

        public static void RunForEachTargetObject(this SerializedProperty serializedProperty, Action<SerializedProperty> action)
        {
            SerializedObject serializedObject = serializedProperty.serializedObject;

            if (!serializedObject.isEditingMultipleObjects)
            {
                action.Invoke(serializedProperty);
                return;
            }

            string propertyPath = serializedProperty.propertyPath;

            for (int i = 0; i < serializedObject.targetObjects.Length; i++)
            {
                UnityEngine.Object targetObject = serializedObject.targetObjects[i];
                SerializedObject targetSerializedObject = new SerializedObject(targetObject);
                SerializedProperty targetProperty = targetSerializedObject.FindProperty(propertyPath);
                action.Invoke(targetProperty);
                targetSerializedObject.ApplyModifiedProperties();
            }
        }
    }
}
#endif
