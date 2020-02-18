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

        protected override void OnPostInitialize()
        {
            base.OnPostInitialize();

            int count = SceneManager.sceneCount;

            for (int i = 0; i < count; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);

                AddController(scene);
            }
        }

        protected override void OnPreUninitialize()
        {
            base.OnPreUninitialize();

            foreach (KeyValuePair<Scene, SceneController> pair in m_controllers)
            {
                pair.Value.Uninitialize();
            }

            m_controllers.Clear();
        }

        public Scene Load(string sceneName, SceneLoadParameters parameters)
        {
            if (string.IsNullOrEmpty(sceneName)) throw new ArgumentException("Value cannot be null or empty.", nameof(sceneName));

            Loading?.Invoke(sceneName, parameters);

            Scene scene = OnLoad(sceneName, parameters);

            AddController(scene);

            Loaded?.Invoke(scene, parameters);

            return scene;
        }

        public async Task<Scene> LoadAsync(string sceneName, SceneLoadParameters parameters)
        {
            if (string.IsNullOrEmpty(sceneName)) throw new ArgumentException("Value cannot be null or empty.", nameof(sceneName));

            Loading?.Invoke(sceneName, parameters);

            Scene scene = await OnLoadAsync(sceneName, parameters);

            AddController(scene);

            Loaded?.Invoke(scene, parameters);

            return scene;
        }

        public void Unload(Scene scene, SceneUnloadParameters parameters)
        {
            Unloading?.Invoke(scene, parameters);

            RemoveController(scene);

            OnUnload(scene, parameters);

            Unloaded?.Invoke(scene, parameters);
        }

        public async Task UnloadAsync(Scene scene, SceneUnloadParameters parameters)
        {
            Unloading?.Invoke(scene, parameters);

            RemoveController(scene);

            await OnUnloadAsync(scene, parameters);

            Unloaded?.Invoke(scene, parameters);
        }

        protected abstract Scene OnLoad(string sceneName, SceneLoadParameters parameters);
        protected abstract Task<Scene> OnLoadAsync(string sceneName, SceneLoadParameters parameters);
        protected abstract void OnUnload(Scene scene, SceneUnloadParameters parameters);
        protected abstract Task OnUnloadAsync(Scene scene, SceneUnloadParameters parameters);

        protected virtual void OnControllerAdded(SceneController controller)
        {
        }

        protected virtual void OnControllerRemove(SceneController controller)
        {
        }

        private void AddController(Scene scene)
        {
            var controller = new SceneController(scene, Context);

            m_controllers.Add(scene, controller);

            controller.Initialize();

            OnControllerAdded(controller);
        }

        private void RemoveController(Scene scene)
        {
            if (m_controllers.TryGetValue(scene, out SceneController controller))
            {
                OnControllerRemove(controller);

                controller.Uninitialize();

                m_controllers.Remove(scene);
            }
        }
    }
}
