﻿using System;
using UGF.CustomSettings.Runtime;

namespace UGF.Module.Scenes.Runtime.Loaders.Manager
{
    public static class ManagerSceneSettings
    {
        public static CustomSettingsPackage<ManagerSceneSettingsAsset> Settings { get; } = new CustomSettingsPackage<ManagerSceneSettingsAsset>
        (
            "UGF.Module.Scenes",
            "ManagerSceneSettings"
        );

        public static string GetScenePath(string id)
        {
            return TryGetScenePath(id, out string path) ? path : throw new ArgumentException($"Scene path not found by the specified id: '{id}'.");
        }

        public static bool TryGetScenePath(string id, out string path)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));

            ManagerSceneSettingsAsset data = Settings.Data;

            for (int i = 0; i < data.Scenes.Count; i++)
            {
                ManagerSceneSettingsAsset.SceneData scene = data.Scenes[i];

                if (scene.Id == id)
                {
                    path = scene.Path;
                    return true;
                }
            }

            path = default;
            return false;
        }
    }
}