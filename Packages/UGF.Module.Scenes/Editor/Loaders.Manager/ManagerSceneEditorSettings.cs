using UGF.CustomSettings.Editor;
using UnityEditor;

namespace UGF.Module.Scenes.Editor.Loaders.Manager
{
    public static class ManagerSceneEditorSettings
    {
        public static CustomSettingsEditorPackage<ManagerSceneEditorSettingsData> Settings { get; } = new CustomSettingsEditorPackage<ManagerSceneEditorSettingsData>
        (
            "UGF.Module.Scenes",
            "ManagerSceneEditorSettings"
        );

        [SettingsProvider]
        private static SettingsProvider GetProvider()
        {
            return new CustomSettingsProvider<ManagerSceneEditorSettingsData>("Project/UGF/Scenes Manager", Settings, SettingsScope.Project);
        }
    }
}
