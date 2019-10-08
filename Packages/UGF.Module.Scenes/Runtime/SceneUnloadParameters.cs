using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime
{
    public struct SceneUnloadParameters
    {
        public UnloadSceneOptions UnloadOptions { get; }
        public bool UnloadUnused { get; }

        public static SceneUnloadParameters Default { get; } = new SceneUnloadParameters(UnloadSceneOptions.None);

        public SceneUnloadParameters(UnloadSceneOptions unloadOptions, bool unloadUnused = false)
        {
            UnloadOptions = unloadOptions;
            UnloadUnused = unloadUnused;
        }
    }
}
