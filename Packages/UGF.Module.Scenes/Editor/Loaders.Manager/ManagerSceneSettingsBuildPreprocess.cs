using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace UGF.Module.Scenes.Editor.Loaders.Manager
{
    internal class ManagerSceneSettingsBuildPreprocess : IPreprocessBuildWithReport
    {
        public int callbackOrder { get; }

        public void OnPreprocessBuild(BuildReport report)
        {
            if (ManagerSceneEditorSettings.ScenesAutoUpdate)
            {
                ManagerSceneEditorSettings.UpdateScenesFromBuildSettings();
            }
        }
    }
}
