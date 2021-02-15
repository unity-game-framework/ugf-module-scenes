using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime
{
    public interface ISceneLoadParameters
    {
        LoadSceneMode AddMode { get; }
        LocalPhysicsMode PhysicsMode { get; }
        bool AllowActivation { get; }
    }
}
