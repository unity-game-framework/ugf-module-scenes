using UGF.CustomSettings.Editor;
using UGF.Module.Scenes.Runtime.Loaders.Manager;
using UnityEditor;

namespace UGF.Module.Scenes.Editor.Loaders.Manager
{
    [InitializeOnLoad]
    public static class ManagerSceneEditorSettings
    {
        public static bool ScenesAutoUpdate
        {
            get { return ManagerSceneSettings.Settings.GetData().ScenesAutoUpdate; }
            set
            {
                ManagerSceneSettings.Settings.GetData().ScenesAutoUpdate = value;
                ManagerSceneSettings.Settings.SaveSettings();
            }
        }

        static ManagerSceneEditorSettings()
        {
            EditorBuildSettings.sceneListChanged += OnEditorBuildSettingsSceneListChanged;
        }

        public static void UpdateScenesFromBuildSettings()
        {
            ManagerSceneSettingsAsset data = ManagerSceneSettings.Settings.GetData();
            EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;

            data.Scenes.Clear();

            foreach (EditorBuildSettingsScene scene in scenes)
            {
                if (scene.enabled)
                {
                    var sceneData = new ManagerSceneSettingsAsset.SceneData
                    {
                        Id = scene.guid.ToString(),
                        Path = scene.path
                    };

                    data.Scenes.Add(sceneData);
                }
            }

            ManagerSceneSettings.Settings.SaveSettings();
        }

        private static void OnEditorBuildSettingsSceneListChanged()
        {
            if (ScenesAutoUpdate)
            {
                UpdateScenesFromBuildSettings();
            }
        }

        [SettingsProvider]
        private static SettingsProvider GetProvider()
        {
            return new CustomSettingsProvider<ManagerSceneSettingsAsset>("Project/UGF/Scenes Manager", ManagerSceneSettings.Settings, SettingsScope.Project);
        }
    }
}
