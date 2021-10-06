using UnityEditor;

namespace UGF.Module.Scenes.Editor.Loaders.Manager
{
    internal static class ManagerSceneEditorProgress
    {
        internal static void StartUpdateSceneGroupAll()
        {
            int progressId = Progress.Start("Update All Manager Scene Groups", string.Empty, Progress.Options.Indefinite);

            try
            {
                ManagerSceneEditorUtility.UpdateSceneGroupAll();

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
    }
}
