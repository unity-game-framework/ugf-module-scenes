using System;
using UGF.Module.Scenes.Runtime.Loaders.Manager;
using UnityEditor;
using Object = UnityEngine.Object;

namespace UGF.Module.Scenes.Editor.Loaders.Manager
{
    public static class ManagerSceneEditorUtility
    {
        public static void UpdateAllSceneGroups()
        {
            int progressId = Progress.Start("Update All Manager Scene Groups");

            try
            {
                string[] guids = AssetDatabase.FindAssets($"t:{nameof(ManagerSceneGroupAsset)}");

                for (int i = 0; i < guids.Length; i++)
                {
                    Progress.Report(progressId, i, guids.Length);

                    string guid = guids[i];
                    string path = AssetDatabase.GUIDToAssetPath(guid);
                    var asset = AssetDatabase.LoadAssetAtPath<ManagerSceneGroupAsset>(path);

                    if (asset != null)
                    {
                        UpdateSceneGroupEntries(asset);

                        EditorUtility.SetDirty(asset);
                    }
                }

                Progress.Finish(progressId);
            }
            catch
            {
                Progress.Finish(progressId, Progress.Status.Failed);
                throw;
            }
            finally
            {
                AssetDatabase.SaveAssets();
            }
        }

        public static bool IsSceneGroupHasMissingEntries(ManagerSceneGroupAsset group)
        {
            if (group == null) throw new ArgumentNullException(nameof(group));

            for (int i = 0; i < group.Scenes.Count; i++)
            {
                ManagerSceneGroupAsset.Entry entry = group.Scenes[i];

                if (!string.IsNullOrEmpty(entry.Id) && !string.IsNullOrEmpty(entry.Address))
                {
                    var asset = AssetDatabase.LoadAssetAtPath<Object>(entry.Address);

                    if (asset == null)
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        public static void UpdateSceneGroupEntries(ManagerSceneGroupAsset group)
        {
            if (group == null) throw new ArgumentNullException(nameof(group));

            for (int i = 0; i < group.Scenes.Count; i++)
            {
                ManagerSceneGroupAsset.Entry entry = group.Scenes[i];

                if (!string.IsNullOrEmpty(entry.Id))
                {
                    string path = AssetDatabase.GUIDToAssetPath(entry.Id);

                    if (!string.IsNullOrEmpty(path))
                    {
                        entry.Address = path;

                        group.Scenes[i] = entry;
                    }
                }
            }
        }
    }
}
