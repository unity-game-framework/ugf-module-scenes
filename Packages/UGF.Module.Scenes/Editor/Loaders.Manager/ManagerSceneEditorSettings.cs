﻿using UGF.CustomSettings.Editor;
using UGF.Module.Scenes.Runtime.Loaders.Manager;
using UnityEditor;
using UnityEngine;

namespace UGF.Module.Scenes.Editor.Loaders.Manager
{
    [InitializeOnLoad]
    public static class ManagerSceneEditorSettings
    {
        static ManagerSceneEditorSettings()
        {
            EditorBuildSettings.sceneListChanged += OnEditorBuildSettingsSceneListChanged;
        }

        public static void UpdateScenesFromBuildSettings()
        {
            ManagerSceneSettingsAsset data = ManagerSceneSettings.Settings.Data;
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
            UpdateScenesFromBuildSettings();
        }

        [SettingsProvider]
        private static SettingsProvider GetProvider()
        {
            return new CustomSettingsProvider<ManagerSceneSettingsAsset>("Project/UGF/Scenes Manager", ManagerSceneSettings.Settings, SettingsScope.Project);
        }
    }
}