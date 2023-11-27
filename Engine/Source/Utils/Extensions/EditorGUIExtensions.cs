#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Popcore.Core.Extensions
{
    public static class EditorGUIExtensions
    {
        public static void IntRangePropertyField(Rect rect, SerializedProperty property, int minValue, int maxValue)
        {
            int value = EditorGUI.IntSlider(rect, property.displayName, property.intValue, minValue, maxValue);
            property.intValue = Mathf.Clamp(value, minValue, maxValue);
        }

        public static void FloatRangePropertyField(Rect rect, SerializedProperty property, float minValue, float maxValue)
        {
            float value = EditorGUI.Slider(rect, property.displayName, property.floatValue, minValue, maxValue);
            property.floatValue = Mathf.Clamp(value, minValue, maxValue);
        }
    }
}
#endif
