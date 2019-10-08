using UGF.Coroutines.Runtime;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime
{
    public interface ISceneLoadCoroutine : ICoroutine<Scene>
    {
        bool AllowSceneActivation { get; }

        void ActivateScene();
    }
}
