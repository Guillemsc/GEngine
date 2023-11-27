#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.Search;
using UnityEngine;

namespace Popcore.Core.Extensions
{
    /// <summary>
    /// Editor only extensions for the AssetDatabase.
    /// </summary>
    public static class AssetDatabaseExtensions
    {
        [Obsolete("This method is obsolete. Use FindAllPrefabsWithComponentTypeAsync instead")]
        public static Task<List<T>> FindGameObjectsByComponentTypeAsync<T>() where T : UnityEngine.Object
        {
            return FindAllPrefabsWithComponentTypeAsync<T>();
        }

        /// <summary>
        /// Using the Unity's <see cref="SearchService"/> does a quick search of all game objects that contain a certain component type.
        /// This may trigger a refresh on the <see cref="SearchService"/>, which can take some time for the first instance.
        /// </summary>
        public static async Task<List<T>> FindAllPrefabsWithComponentTypeAsync<T>() where T : UnityEngine.Object
        {
            List<T> ret = new List<T>();

            Type type = typeof(T);
            IList<SearchItem> results = await SearchServiceExtensions.RequestAsync($"p: t:prefab t:{type.Name}");

            foreach (SearchItem result in results)
            {
                T casted = (T)result.ToObject(typeof(T));

                ret.Add(casted);
            }

            return ret;
        }

        /// <summary>
        /// Finds all prefabs in the project that contain a specified component type on the root.
        /// </summary>
        /// <param name="type">The type of component to search for.</param>
        /// <returns>A list of UnityEngine.Object representing the prefabs with the specified component type.</returns>
        public static List<UnityEngine.Object> FindAllPrefabsWithComponentType(Type type)
        {
            List<UnityEngine.Object> ret = new();

            string[] prefabPaths = AssetDatabase.FindAssets("t:prefab");

            foreach (string assetPath in prefabPaths)
            {
                bool assetFound = TryFindMainAssetByGuid(
                    assetPath,
                    out UnityEngine.Object prefabObject
                );

                if (!assetFound)
                {
                    continue;
                }

                if (prefabObject is not GameObject gameObject)
                {
                    continue;
                }

                bool componentFound = gameObject.HasComponent(type);

                if (!componentFound)
                {
                    continue;
                }

                ret.Add(gameObject);
            }

            return ret;
        }

        public static bool TryFindFirstPrefabWithComponentType(
            Type type,
            out UnityEngine.Object obj
            )
        {
            List<UnityEngine.Object> prefabs = FindAllPrefabsWithComponentType(type);

            return prefabs.TryGet(0, out obj);
        }

        /// <summary>
        /// Using <see cref="AssetDatabase.FindAssets(string)"/>, searches for all the assets of a certain type
        /// that match the filter with the provided name.
        /// </summary>
        public static List<T> FindAssetsByTypeAndNameAtFolders<T>(string name, params string[] folders) where T : UnityEngine.Object
        {
            Type type = typeof(T);
            string fullFilter = $"{name} t:{type.Name}";

            string[] guids = AssetDatabase.FindAssets(fullFilter, folders);

            List<T> assets = new List<T>();
            foreach (string guid in guids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                T asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);

                if (asset != null)
                {
                    assets.Add(asset);
                }
            }

            return assets;
        }

        /// <summary>
        /// Using <see cref="AssetDatabase.FindAssets(string)"/>, searches for all the assets of a certain type
        /// that match the filter with the provided name.
        /// </summary>
        public static List<T> FindAssetsByTypeAndName<T>(string name) where T : UnityEngine.Object
        {
            return FindAssetsByTypeAndNameAtFolders<T>(name, Array.Empty<string>());
        }

        /// <summary>
        /// Using <see cref="AssetDatabase.FindAssets(string)"/>, searches for all the assets of a certain type
        /// that match the filter with the provided name.
        /// </summary>
        public static List<UnityEngine.Object> FindAssetsByTypeAndName(Type type, string name)
        {
            return FindAssetsByTypeNameAndName(type.Name, name);
        }

        /// <summary>
        /// Using <see cref="AssetDatabase.FindAssets(string)"/>, searches for all the assets of a certain type.
        /// </summary>
        public static List<UnityEngine.Object> FindAssetsByType(Type type)
        {
            return FindAssetsByTypeAndName(type, string.Empty);
        }

        /// <summary>
        /// Using <see cref="AssetDatabase.FindAssets(string)"/>, searches for all the assets of a certain
        /// type by the type name and an optional name.
        /// </summary>
        public static List<UnityEngine.Object> FindAssetsByTypeNameAndName(string typeName, string name)
        {
            return FindAssetsByTypeNameAndNameAtFolders(typeName, name);
        }

        /// <summary>
        /// Using <see cref="AssetDatabase.FindAssets(string)"/>, searches for all the assets of a certain
        /// type by the type name and an optional name.
        /// </summary>
        public static List<UnityEngine.Object> FindAssetsByTypeNameAndNameAtFolders(string typeName, string name, params string[] searchInFolders)
        {
            string fullFilter = $"{name} t:{typeName}";

            string[] guids = AssetDatabase.FindAssets(fullFilter, searchInFolders);

            List<UnityEngine.Object> assets = new();
            foreach (string guid in guids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                UnityEngine.Object asset = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(assetPath);

                if (asset != null)
                {
                    assets.Add(asset);
                }
            }

            return assets;
        }

        /// <summary>
        /// Using <see cref="AssetDatabase.FindAssets(string)"/>, searches for all the assets of a certain type by the type name.
        /// </summary>
        public static List<UnityEngine.Object> FindAssetsByTypeName(string typeName)
        {
            return FindAssetsByTypeNameAndName(typeName, string.Empty);
        }

        public static bool TryFindFirstAssetsByTypeName(string typeName, out UnityEngine.Object asset)
        {
            List<UnityEngine.Object> objects = FindAssetsByTypeName(typeName);

            return objects.TryGet(0, out asset);
        }

        /// <summary>
        /// Using <see cref="AssetDatabase.FindAssets(string)"/>, searches for all the assets of a certain type
        /// that match the filter with the provided name. Then returns the first one.
        /// </summary>
        public static bool TryFindFirstAssetByTypeAndName<T>(string name, out T asset) where T : UnityEngine.Object
        {
            Type type = typeof(T);
            string fullFilter = $"{name} t:{type.Name}";

            string[] guids = AssetDatabase.FindAssets(fullFilter);

            foreach (var guid in guids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);

                if (asset != null)
                {
                    return true;
                }
            }

            asset = default;
            return false;
        }

        /// <summary>
        /// Using <see cref="AssetDatabase.FindAssets(string)"/>, searches for all the assets of a certain type
        /// that match the filter with the provided name. Then returns the first one.
        /// </summary>
        public static bool TryFindFirstAssetByTypeAndName(Type type, string name, out UnityEngine.Object asset)
        {
            string fullFilter = $"{name} t:{type.Name}";

            string[] guids = AssetDatabase.FindAssets(fullFilter);

            foreach (var guid in guids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                asset = AssetDatabase.LoadAssetAtPath(assetPath, type);

                if (asset != null)
                {
                    return true;
                }
            }

            asset = default;
            return false;
        }

        /// <summary>
        /// Using <see cref="AssetDatabase.FindAssets(string)"/>, searches for all the assets of a certain type.
        /// </summary>
        public static List<T> FindAssetsByType<T>() where T : UnityEngine.Object
        {
            return FindAssetsByTypeAndName<T>(string.Empty);
        }

        /// <summary>
        /// Using <see cref="AssetDatabase.FindAssets(string)"/>, searches for all the assets of a certain type.
        /// </summary>
        public static List<T> FindAssetsByTypeAtFolders<T>(params string[] folders) where T : UnityEngine.Object
        {
            return FindAssetsByTypeAndNameAtFolders<T>(string.Empty, folders);
        }

        /// <summary>
        /// Using <see cref="AssetDatabase.FindAssets(string)"/>, searches for all the assets of a certain type.
        /// Then returns the first one.
        /// </summary>
        public static bool TryFindFirstAssetByType<T>(out T asset) where T : UnityEngine.Object
        {
            return TryFindFirstAssetByTypeAndName(string.Empty, out asset);
        }

        /// <summary>
        /// Using <see cref="AssetDatabase.FindAssets(string)"/>, searches for all the assets of a certain type.
        /// Then returns the first one.
        /// </summary>
        public static bool TryFindFirstAssetByType(Type type, out UnityEngine.Object asset)
        {
            return TryFindFirstAssetByTypeAndName(type, string.Empty, out asset);
        }

        /// <summary>
        /// Tries to find the first asset of a specified type by its name or, if not found, returns the first asset of that type.
        /// </summary>
        /// <param name="type">The type of asset to search for.</param>
        /// <param name="name">The name of the asset to search for.</param>
        /// <param name="asset">The found asset, if any.</param>
        /// <returns><c>true</c> if the asset was found; otherwise, <c>false</c>.</returns>
        public static bool TryFindFirstAssetByTypeAndNameOrFirstByType(Type type, string name, out UnityEngine.Object asset)
        {
            bool found = TryFindFirstAssetByTypeAndName(type, name, out asset);

            if (found)
            {
                return true;
            }

            return TryFindFirstAssetByType(type, out asset);
        }

        /// <summary>
        /// Using <see cref="AssetDatabase.FindAssets(string)"/>, searches for all the assets of a certain type.
        /// Then returns the first one.
        /// </summary>
        /// <exception cref="InvalidOperationException"> When no asset could be found.</exception>
        public static T FindAssetByTypeOrFail<T>() where T : UnityEngine.Object
        {
            if (!TryFindFirstAssetByType<T>(out var asset))
            {
                throw new InvalidOperationException($"Asset of type {typeof(T).Name} could not be found");
            }

            return asset;
        }

        /// <summary>
        /// Searches for the first asset matching the provided guid.
        /// </summary>
        public static bool TryFindAssetByGuid<T>(string guid, out T asset) where T : UnityEngine.Object
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);

            if (string.IsNullOrEmpty(assetPath))
            {
                asset = default;
                return false;
            }

            return TryLoadAssetAtPath(assetPath, out asset);
        }

        /// <summary>
        /// Searches for the first asset matching the provided guid.
        /// </summary>
        public static bool TryFindMainAssetByGuid(string guid, out UnityEngine.Object asset)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);

            if (string.IsNullOrEmpty(assetPath))
            {
                asset = default;
                return false;
            }

            return TryLoadMainAssetAtPath(assetPath, out asset);
        }

        /// <summary>
        /// Searches for the first asset matching the provided guid.
        /// </summary>
        public static bool TryGetAssetGuid(UnityEngine.Object asset, out string guid)
        {
            string assetPath = AssetDatabase.GetAssetPath(asset);

            if (string.IsNullOrEmpty(assetPath))
            {
                guid = default;
                return false;
            }

            guid = AssetDatabase.AssetPathToGUID(assetPath);
            return string.IsNullOrEmpty(guid);
        }

        /// <summary>
        /// Searches for the first asset matching the provided asset path (see also: AssetDatabase.LoadAssetAtPath).
        /// </summary>
        public static bool TryLoadAssetAtPath<T>(string assetPath, out T asset) where T : UnityEngine.Object
        {
            asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
            return asset != null;
        }

        /// <summary>
        /// Searches for the first asset matching the provided asset path (see also: AssetDatabase.LoadMainAssetAtPath).
        /// </summary>
        public static bool TryLoadMainAssetAtPath(string assetPath, out UnityEngine.Object asset)
        {
            asset = AssetDatabase.LoadMainAssetAtPath(assetPath);
            return asset != null;
        }

        /// <summary>
        ///  Removes all objects from an asset (see also: AssetDatabase.RemoveObjectFromAsset).
        /// </summary>
        public static void RemoveAllSubAssets(UnityEngine.Object obj)
        {
            string assetPath = AssetDatabase.GetAssetPath(obj);

            UnityEngine.Object[] assets = AssetDatabase.LoadAllAssetsAtPath(assetPath);

            foreach (var asset in assets)
            {
                if (!AssetDatabase.IsSubAsset(asset))
                {
                    continue;
                }

                AssetDatabase.RemoveObjectFromAsset(asset);
            }
        }

        /// <summary>
        /// Using <see cref="AssetDatabase.FindAssets(string, string[])"/>, searches for all the assets on a folder.
        /// </summary>
        public static string[] GetAssetsGuidAtPath(string folder)
        {
            return AssetDatabase.FindAssets(string.Empty, new[] { folder });
        }

        public static List<string> GetAllScenesPaths()
        {
            string[] scenesGUIDs = AssetDatabase.FindAssets("t:Scene");

            List<string> scenesPaths = scenesGUIDs
                .Select(AssetDatabase.GUIDToAssetPath)
                .Where(s => s.StartsWith("Assets/"))
                .ToList();

            return scenesPaths;
        }

        public static bool TryGetScenePathFromSceneName(string sceneName, out string foundScenePath)
        {
            List<string> scenesPaths = GetAllScenesPaths();

            foreach(string scenePath in scenesPaths)
            {
                string currentSceneName = Path.GetFileNameWithoutExtension(scenePath);

                bool isScene = string.Equals(currentSceneName, sceneName);

                if(!isScene)
                {
                    continue;
                }

                foundScenePath = scenePath;
                return true;
            }

            foundScenePath = string.Empty;
            return false;
        }
    }
}
#endif
