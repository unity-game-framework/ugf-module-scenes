using System.Threading.Tasks;
using UGF.Application.Runtime;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime
{
    public interface ISceneModule : IApplicationModule
    {
        Scene LoadScene(string sceneName, LoadSceneParameters parameters);
        Task<Scene> LoadSceneAsync(string sceneName, LoadSceneParameters parameters);
        void UnloadScene(Scene scene, UnloadSceneOptions unloadOptions);
        Task UnloadSceneAsync(Scene scene, UnloadSceneOptions unloadOptions);
    }
}
