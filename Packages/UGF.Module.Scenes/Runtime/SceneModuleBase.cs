using System;
using System.Threading.Tasks;
using UGF.Application.Runtime;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime
{
    public abstract class SceneModuleBase : ApplicationModuleBase, ISceneModule
    {
        public event SceneLoadingHandler Loading;
        public event SceneLoadHandler Loaded;
        public event SceneUnloadHandler Unloading;
        public event SceneUnloadHandler Unloaded;

        public Scene LoadScene(string sceneName, SceneLoadParameters parameters)
        {
            if (string.IsNullOrEmpty(sceneName)) throw new ArgumentException("Value cannot be null or empty.", nameof(sceneName));

            Loading?.Invoke(sceneName, parameters);

            Scene scene = OnLoadScene(sceneName, parameters);

            Loaded?.Invoke(scene, parameters);

            return scene;
        }

        public async Task<Scene> LoadSceneAsync(string sceneName, SceneLoadParameters parameters)
        {
            if (string.IsNullOrEmpty(sceneName)) throw new ArgumentException("Value cannot be null or empty.", nameof(sceneName));

            Loading?.Invoke(sceneName, parameters);

            Scene scene = await OnLoadSceneAsync(sceneName, parameters);

            Loaded?.Invoke(scene, parameters);

            return scene;
        }

        public void UnloadScene(Scene scene, SceneUnloadParameters parameters)
        {
            Unloading?.Invoke(scene, parameters);

            OnUnloadScene(scene, parameters);

            Unloaded?.Invoke(scene, parameters);
        }

        public async Task UnloadSceneAsync(Scene scene, SceneUnloadParameters parameters)
        {
            Unloading?.Invoke(scene, parameters);

            await OnUnloadSceneAsync(scene, parameters);

            Unloaded?.Invoke(scene, parameters);
        }

        protected abstract Scene OnLoadScene(string sceneName, SceneLoadParameters parameters);
        protected abstract Task<Scene> OnLoadSceneAsync(string sceneName, SceneLoadParameters parameters);
        protected abstract void OnUnloadScene(Scene scene, SceneUnloadParameters parameters);
        protected abstract Task OnUnloadSceneAsync(Scene scene, SceneUnloadParameters parameters);
    }
}
