using System.Collections.Generic;
using System.Threading.Tasks;
using UGF.Application.Runtime;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime
{
    public interface ISceneModule : IApplicationModule
    {
        IReadOnlyDictionary<Scene, SceneController> Controllers { get; }

        event SceneLoadingHandler Loading;
        event SceneLoadHandler Loaded;
        event SceneUnloadHandler Unloading;
        event SceneUnloadHandler Unloaded;
        event SceneControllerHandler ControllerAdd;
        event SceneControllerHandler ControllerRemove;

        Scene Load(string sceneName, SceneLoadParameters parameters);
        Task<Scene> LoadAsync(string sceneName, SceneLoadParameters parameters);
        void Unload(Scene scene, SceneUnloadParameters parameters);
        Task UnloadAsync(Scene scene, SceneUnloadParameters parameters);
        SceneController GetController(string sceneName);
        SceneController GetController(Scene scene);
        bool TryGetController(string sceneName, out SceneController controller);
        bool TryGetController(Scene scene, out SceneController controller);
    }
}
