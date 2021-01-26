using UGF.CustomSettings.Editor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace UGF.Module.Scenes.Editor.Loaders.Manager
{
    internal class ManagerSceneEditorBuildPreprocess : IPreprocessBuildWithReport
    {
        public int callbackOrder { get; }

        public void OnPreprocessBuild(BuildReport report)
        {
            CustomSettingsEditorPackage<ManagerSceneEditorSettingsData> settings = ManagerSceneEditorSettings.Settings;

            if (settings.Exists() && settings.GetData().UpdateAllGroupsOnBuild)
            {
                ManagerSceneEditorUtility.UpdateAllSceneGroups();
            }
        }
    }
}
