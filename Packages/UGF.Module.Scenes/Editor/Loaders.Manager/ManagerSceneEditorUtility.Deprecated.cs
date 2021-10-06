using System;

namespace UGF.Module.Scenes.Editor.Loaders.Manager
{
    public static partial class ManagerSceneEditorUtility
    {
        [Obsolete("UpdateAllSceneGroups has been deprecated. Use UpdateSceneGroupAll method instead.")]
        public static void UpdateAllSceneGroups()
        {
            ManagerSceneEditorProgress.StartUpdateSceneGroupAll();
        }
    }
}
