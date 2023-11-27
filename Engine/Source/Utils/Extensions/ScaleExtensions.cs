using System.Numerics;

namespace GEngine.Utils.Extensions
{
    public static class ScaleExtensions
    {
        /// <summary>
        /// From a bounding size, and the element size, calculates a new size to fit the bounding box, keeping the
        /// original aspect ratio.
        /// </summary>
        /// <param name="boundingSize">Size of the bounding box.</param>
        /// <param name="elementSize">Size of the element to keep the aspect ratio.</param>
        /// <returns>New size.</returns>
        public static Vector2 GetPreservedAspectRatioSize(Vector2 boundingSize, Vector2 elementSize)
        {
            float scale = GetScaleToFitInParent(boundingSize, elementSize);
            return elementSize * scale;
        }

        /// <summary>
        /// From a bounding size, and the element size, calculates a new size to fit the bounding box, keeping the
        /// original aspect ratio if requested.
        /// </summary>
        /// <param name="boundingSize">Size of the bounding box.</param>
        /// <param name="elementSize">Size of the element to resize.</param>
        /// <param name="shouldPreserveAspect">If should or should not mantain aspect ratio when resizing.</param>
        /// <returns>New size.</returns>
        public static Vector2 GetConditionallyPreservedAspectRatioSize(
            Vector2 boundingSize,
            Vector2 elementSize,
            bool shouldPreserveAspect
            )
        {
            if (!shouldPreserveAspect)
            {
                return boundingSize;
            }

            return GetPreservedAspectRatioSize(boundingSize, elementSize);
        }

        /// <summary>
        /// Gets the scale to apply to an element size, so the whole element fits inside a bounding box.
        /// </summary>
        /// <param name="boundingSize">Size of the bounding box.</param>
        /// <param name="elementSize">Size of the element to resize.</param>
        /// <returns>Scale to apply to an element size.</returns>
        public static float GetScaleToFitInParent(Vector2 boundingSize, Vector2 elementSize)
        {
            Vector2 scale = boundingSize / elementSize;
            float minScale = Math.Min(scale.X, scale.Y);
            return minScale;
        }
    }
}
