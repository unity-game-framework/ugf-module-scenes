using System;
using UGF.Module.Scenes.Runtime;
using UGF.Module.Scenes.Runtime.Loaders.Manager;
using UnityEditor;

namespace UGF.Module.Scenes.Editor.Loaders.Manager
{
    public static class ManagerSceneEditorUtility
    {
        public static void UpdateSceneGroupAll()
        {
            string[] guids = AssetDatabase.FindAssets($"t:{nameof(ManagerSceneGroupAsset)}");

            for (int i = 0; i < guids.Length; i++)
            {
                string guid = guids[i];
                string path = AssetDatabase.GUIDToAssetPath(guid);
                var asset = AssetDatabase.LoadAssetAtPath<ManagerSceneGroupAsset>(path);

                if (asset != null)
                {
                    UpdateSceneGroupEntries(asset);

                    EditorUtility.SetDirty(asset);
                }
            }
        }

        public static void UpdateSceneGroupEntries(ManagerSceneGroupAsset group)
        {
            if (group == null) throw new ArgumentNullException(nameof(group));

            for (int i = 0; i < group.Scenes.Count; i++)
            {
                SceneReference reference = group.Scenes[i];

                if (reference.HasGuid && SceneReferenceEditorUtility.TryUpdateScenePath(reference, out string path))
                {
                    group.Scenes[i] = new SceneReference(reference.Guid, path);
                }
            }
        }
    }
}
