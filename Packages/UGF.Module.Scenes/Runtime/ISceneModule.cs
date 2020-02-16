using System.Threading.Tasks;
using UGF.Application.Runtime;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime
{
    public interface ISceneModule : IApplicationModule
    {
        event SceneLoadingHandler Loading;
        event SceneLoadHandler Loaded;
        event SceneUnloadHandler Unloading;
        event SceneUnloadHandler Unloaded;

        Scene LoadScene(string sceneName, SceneLoadParameters parameters);
        Task<Scene> LoadSceneAsync(string sceneName, SceneLoadParameters parameters);
        void UnloadScene(Scene scene, SceneUnloadParameters parameters);
        Task UnloadSceneAsync(Scene scene, SceneUnloadParameters parameters);
    }
}
