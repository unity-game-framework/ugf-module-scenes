using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using UGF.Application.Runtime;
using UGF.Elements.Runtime;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime
{
    public abstract class SceneModuleBase : ApplicationModuleBase, ISceneModule
    {
        public IElementContext Context { get; }
        public IReadOnlyDictionary<Scene, SceneController> Controllers { get; }

        public event SceneLoadingHandler Loading;
        public event SceneLoadHandler Loaded;
        public event SceneUnloadHandler Unloading;
        public event SceneUnloadHandler Unloaded;

        private readonly Dictionary<Scene, SceneController> m_controllers = new Dictionary<Scene, SceneController>();

        protected SceneModuleBase(IElementContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            Controllers = new ReadOnlyDictionary<Scene, SceneController>(m_controllers);
        }

        public Scene LoadScene(string sceneName, SceneLoadParameters parameters)
        {
            if (string.IsNullOrEmpty(sceneName)) throw new ArgumentException("Value cannot be null or empty.", nameof(sceneName));

            Loading?.Invoke(sceneName, parameters);

            Scene scene = OnLoadScene(sceneName, parameters);

            AddController(scene);

            Loaded?.Invoke(scene, parameters);

            return scene;
        }

        public async Task<Scene> LoadSceneAsync(string sceneName, SceneLoadParameters parameters)
        {
            if (string.IsNullOrEmpty(sceneName)) throw new ArgumentException("Value cannot be null or empty.", nameof(sceneName));

            Loading?.Invoke(sceneName, parameters);

            Scene scene = await OnLoadSceneAsync(sceneName, parameters);

            AddController(scene);

            Loaded?.Invoke(scene, parameters);

            return scene;
        }

        public void UnloadScene(Scene scene, SceneUnloadParameters parameters)
        {
            Unloading?.Invoke(scene, parameters);

            RemoveController(scene);

            OnUnloadScene(scene, parameters);

            Unloaded?.Invoke(scene, parameters);
        }

        public async Task UnloadSceneAsync(Scene scene, SceneUnloadParameters parameters)
        {
            Unloading?.Invoke(scene, parameters);

            RemoveController(scene);

            await OnUnloadSceneAsync(scene, parameters);

            Unloaded?.Invoke(scene, parameters);
        }

        protected abstract Scene OnLoadScene(string sceneName, SceneLoadParameters parameters);
        protected abstract Task<Scene> OnLoadSceneAsync(string sceneName, SceneLoadParameters parameters);
        protected abstract void OnUnloadScene(Scene scene, SceneUnloadParameters parameters);
        protected abstract Task OnUnloadSceneAsync(Scene scene, SceneUnloadParameters parameters);

        private void AddController(Scene scene)
        {
            var controller = new SceneController(scene, Context);

            m_controllers.Add(scene, controller);

            controller.Initialize();
        }

        private void RemoveController(Scene scene)
        {
            if (m_controllers.TryGetValue(scene, out SceneController controller))
            {
                controller.Uninitialize();

                m_controllers.Remove(scene);
            }
        }
    }
}
