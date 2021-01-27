using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime
{
    public interface ISceneUnloadParameters
    {
        UnloadSceneOptions Options { get; }
    }
}
