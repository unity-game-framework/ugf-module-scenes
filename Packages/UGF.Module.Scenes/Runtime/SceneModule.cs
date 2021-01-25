using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using UGF.Application.Runtime;
using UGF.Logs.Runtime;
using UGF.RuntimeTools.Runtime.Providers;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime
{
    public partial class SceneModule : ApplicationModule<SceneModuleDescription>, ISceneModule
    {
        public ISceneProvider Provider { get; }
        public IReadOnlyDictionary<Scene, SceneInstance> Scenes { get; }

        ISceneModuleDescription ISceneModule.Description { get { return Description; } }

        public event SceneLoadHandler Loading;
        public event SceneLoadedHandler Loaded;
        public event SceneUnloadHandler Unloading;
        public event SceneUnloadedHandler Unloaded;

        private readonly Dictionary<Scene, SceneInstance> m_scenes = new Dictionary<Scene, SceneInstance>();

        public SceneModule(SceneModuleDescription description, IApplication application) : this(description, application, new SceneProvider())
        {
        }

        public SceneModule(SceneModuleDescription description, IApplication application, ISceneProvider provider) : base(description, application)
        {
            Provider = provider ?? throw new ArgumentNullException(nameof(provider));
            Scenes = new ReadOnlyDictionary<Scene, SceneInstance>(m_scenes);
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            foreach (KeyValuePair<string, ISceneLoader> pair in Description.Loaders)
            {
                Provider.AddLoader(pair.Key, pair.Value);
            }

            foreach (KeyValuePair<string, ISceneInfo> pair in Description.Scenes)
            {
                Provider.AddScene(pair.Key, pair.Value);
            }

            Log.Debug("Scene Module initialized", new
            {
                loadersCount = Provider.Loaders.Count,
                scenesCount = Provider.Scenes.Count
            });
        }

        protected override void OnUninitialize()
        {
            base.OnUninitialize();

            if (Description.UnloadTrackedScenesOnUninitialize)
            {
                Log.Debug("Scene Module unload tracked scenes on uninitialize", new
                {
                    count = m_scenes.Count
                });

                while (m_scenes.Count > 0)
                {
                    KeyValuePair<Scene, SceneInstance> pair = m_scenes.First();

                    Unload(pair.Value.Id, pair.Key, SceneUnloadParameters.Default);
                }
            }

            m_scenes.Clear();

            foreach (KeyValuePair<string, ISceneLoader> pair in Description.Loaders)
            {
                Provider.RemoveLoader(pair.Key);
            }

            foreach (KeyValuePair<string, ISceneInfo> pair in Description.Scenes)
            {
                Provider.RemoveScene(pair.Key);
            }
        }

        public Scene Load(string id, SceneLoadParameters parameters)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));

            LogSceneLoad(id, parameters);

            Loading?.Invoke(id, parameters);

            Scene scene = OnLoad(id, parameters);
            SceneInstance instance = OnAddScene(id, scene, parameters);

            m_scenes.Add(scene, instance);

            Loaded?.Invoke(id, scene, parameters);

            LogSceneLoaded(id, scene, parameters);

            return scene;
        }

        public async Task<Scene> LoadAsync(string id, SceneLoadParameters parameters)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));

            LogSceneLoad(id, parameters, true);

            Loading?.Invoke(id, parameters);

            Scene scene = await OnLoadAsync(id, parameters);
            SceneInstance instance = OnAddScene(id, scene, parameters);

            m_scenes.Add(scene, instance);

            Loaded?.Invoke(id, scene, parameters);

            LogSceneLoaded(id, scene, parameters, true);

            return scene;
        }

        public void Unload(string id, Scene scene, SceneUnloadParameters parameters)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));

            LogSceneUnload(id, scene, parameters);

            Unloading?.Invoke(id, scene, parameters);

            OnRemoveScene(id, scene, parameters);
            OnUnload(id, scene, parameters);

            m_scenes.Remove(scene);

            Unloaded?.Invoke(id, parameters);

            LogSceneUnloaded(id, parameters);
        }

        public async Task UnloadAsync(string id, Scene scene, SceneUnloadParameters parameters)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));

            LogSceneUnload(id, scene, parameters, true);

            Unloading?.Invoke(id, scene, parameters);

            OnRemoveScene(id, scene, parameters);
            await OnUnloadAsync(id, scene, parameters);

            m_scenes.Remove(scene);

            Unloaded?.Invoke(id, parameters);

            LogSceneUnloaded(id, parameters, true);
        }

        public SceneInstance GetScene(Scene scene)
        {
            return TryGetScene(scene, out SceneInstance instance) ? instance : throw new ArgumentException($"Scene instance not found by the specified scene: '{scene.name}'.");
        }

        public bool TryGetScene(Scene scene, out SceneInstance instance)
        {
            return m_scenes.TryGetValue(scene, out instance);
        }

        protected virtual SceneInstance OnAddScene(string id, Scene scene, SceneLoadParameters parameters)
        {
            if (Description.RegisterApplicationForScenes && ProviderInstance.TryGet(out IProvider<Scene, IApplication> provider))
            {
                provider.Add(scene, Application);
            }

            return new SceneInstance(scene, id);
        }

        protected virtual void OnRemoveScene(string id, Scene scene, SceneUnloadParameters parameters)
        {
            if (Description.RegisterApplicationForScenes && ProviderInstance.TryGet(out IProvider<Scene, IApplication> provider))
            {
                provider.Remove(scene);
            }
        }

        protected virtual Scene OnLoad(string id, SceneLoadParameters parameters)
        {
            ISceneLoader loader = GetLoaderByScene(id);

            Scene scene = loader.Load(Provider, id, parameters);

            return scene;
        }

        protected virtual Task<Scene> OnLoadAsync(string id, SceneLoadParameters parameters)
        {
            ISceneLoader loader = GetLoaderByScene(id);

            Task<Scene> task = loader.LoadAsync(Provider, id, parameters);

            return task;
        }

        protected virtual void OnUnload(string id, Scene scene, SceneUnloadParameters parameters)
        {
            ISceneLoader loader = GetLoaderByScene(id);

            loader.Unload(Provider, id, scene, parameters);
        }

        protected virtual Task OnUnloadAsync(string id, Scene scene, SceneUnloadParameters parameters)
        {
            ISceneLoader loader = GetLoaderByScene(id);

            Task task = loader.UnloadAsync(Provider, id, scene, parameters);

            return task;
        }

        protected ISceneLoader GetLoaderByScene(string id)
        {
            return TryGetLoaderByScene(id, out ISceneLoader loader) ? loader : throw new ArgumentException($"Scene loader not found by the specified scene id: '{id}'.");
        }

        protected bool TryGetLoaderByScene(string id, out ISceneLoader loader)
        {
            loader = default;
            return Provider.TryGetScene(id, out ISceneInfo scene) && Provider.TryGetLoader(scene.LoaderId, out loader);
        }
    }
}
