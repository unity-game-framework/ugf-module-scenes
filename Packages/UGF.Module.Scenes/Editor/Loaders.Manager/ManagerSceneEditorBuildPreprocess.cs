using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace UGF.Module.Scenes.Editor.Loaders.Manager
{
    internal class ManagerSceneEditorBuildPreprocess : IPreprocessBuildWithReport
    {
        public int callbackOrder { get; }

        public void OnPreprocessBuild(BuildReport report)
        {
            if (ManagerSceneEditorSettings.Settings.Exists() && ManagerSceneEditorSettings.Settings.GetData().UpdateAllGroupsOnBuild)
            {
                ManagerSceneEditorUtility.UpdateAllSceneGroups();
            }
        }
    }
}
