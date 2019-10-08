using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime
{
    public struct SceneLoadParameters
    {
        public LoadSceneMode Mode { get; }
        public bool Activate { get; }
        public LocalPhysicsMode PhysicsMode { get; }
        public bool UnloadUnused { get; }

        public static SceneLoadParameters Default { get; } = new SceneLoadParameters(LoadSceneMode.Single);

        public SceneLoadParameters(LoadSceneMode mode, bool activate = true, LocalPhysicsMode physicsMode = LocalPhysicsMode.None, bool unloadUnused = false) : this()
        {
            Mode = mode;
            Activate = activate;
            PhysicsMode = physicsMode;
            UnloadUnused = unloadUnused;
        }
    }
}
