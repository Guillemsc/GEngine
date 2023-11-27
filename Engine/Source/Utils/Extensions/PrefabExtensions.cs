#if UNITY_EDITOR
using UnityEditor.SceneManagement;

namespace Popcore.Core.Extensions
{
    public static class PrefabExtensions
    {
        /// <summary>
        /// Returns if Unity is on prefab mode for the provided asset.
        /// This happens when you select a prefab and your are on prefab editing mode.
        /// </summary>
        public static bool IsPrefabInContext(string assetPath)
        {
            PrefabStage prefabStage = PrefabStageUtility.GetCurrentPrefabStage();

            if (prefabStage == null)
            {
                return false;
            }

            if (!string.Equals(prefabStage.assetPath, assetPath))
            {
                return false;
            }

            return prefabStage.mode == PrefabStage.Mode.InContext;
        }
    }
}
#endif
