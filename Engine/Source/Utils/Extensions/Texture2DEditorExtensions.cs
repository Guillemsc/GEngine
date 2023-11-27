#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Popcore.Core.Extensions
{
    public static class Texture2DEditorExtensions
    {
        /// <summary>
        /// Retrieves an array of Color pixels from a Texture2D. This method is intended for editor scripts and handles the necessary modifications to the texture's import settings to make it readable.
        /// </summary>
        /// <param name="originalTexture">The original Texture2D from which to retrieve the pixels.</param>
        /// <param name="minX">The minimum X-coordinate of the pixel region to retrieve.</param>
        /// <param name="minY">The minimum Y-coordinate of the pixel region to retrieve.</param>
        /// <param name="width">The width of the pixel region to retrieve.</param>
        /// <param name="height">The height of the pixel region to retrieve.</param>
        /// <returns>An array of Color pixels representing the specified region of the texture.</returns>
        public static Color[] GetPixelsEditor(this Texture2D originalTexture, int minX, int minY, int width, int height)
        {
            // Get pixel data
            bool isReadable = originalTexture.isReadable;
            TextureImporter ti = null;
            try
            {
                // Ensure original texture is readable
                if (!isReadable)
                {
                    var origTexPath = AssetDatabase.GetAssetPath(originalTexture);
                    ti = (TextureImporter)AssetImporter.GetAtPath(origTexPath);
                    ti.isReadable = true;
                    ti.SaveAndReimport();
                }

                Color[] pixelData = originalTexture.GetPixels(minX, minY, width, height);
                return pixelData;
            }
            finally
            {
                // Revert
                if (!isReadable && ti != null)
                {
                    ti.isReadable = false;
                    ti.SaveAndReimport();
                }
            }
        }
    }
}
#endif
