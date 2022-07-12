using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.Ids;
using UGF.Logs.Runtime;
using UGF.RuntimeTools.Runtime.Contexts;
using UGF.RuntimeTools.Runtime.Providers;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime
{
    public partial class SceneModule : ApplicationModule<SceneModuleDescription>, ISceneModule
    {
        public IProvider<GlobalId, ISceneLoader> Loaders { get; }
        public IProvider<GlobalId, ISceneInfo> Scenes { get; }
        public IProvider<Scene, SceneInstance> Instances { get; } = new Provider<Scene, SceneInstance>();
        public IContext Context { get; } = new Context();

        ISceneModuleDescription ISceneModule.Description { get { return Description; } }

        public event SceneLoadHandler Loading;
        public event SceneLoadedHandler Loaded;
        public event SceneUnloadHandler Unloading;
        public event SceneUnloadedHandler Unloaded;

        public SceneModule(SceneModuleDescription description, IApplication application) : this(description, application, new Provider<GlobalId, ISceneLoader>(), new Provider<GlobalId, ISceneInfo>())
        {
        }

        public SceneModule(SceneModuleDescription description, IApplication application, IProvider<GlobalId, ISceneLoader> loaders, IProvider<GlobalId, ISceneInfo> scenes) : base(description, application)
        {
            Loaders = loaders ?? throw new ArgumentNullException(nameof(loaders));
            Scenes = scenes ?? throw new ArgumentNullException(nameof(scenes));

            Context.Add(Application);
            Context.Add(Loaders);
            Context.Add(Scenes);
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            foreach ((GlobalId key, ISceneLoader value) in Description.Loaders)
            {
                Loaders.Add(key, value);
            }

            foreach ((GlobalId key, ISceneInfo value) in Description.Scenes)
            {
                Scenes.Add(key, value);
            }

            Log.Debug("Scene Module initialized", new
            {
                loadersCount = Loaders.Entries.Count,
                scenesCount = Scenes.Entries.Count
            });
        }

        protected override void OnUninitialize()
        {
            base.OnUninitialize();

            if (Description.UnloadTrackedScenesOnUninitialize)
            {
                Log.Debug("Scene Module unload tracked scenes on uninitialize", new
                {
                    count = Instances.Entries.Count
                });

                while (Instances.Entries.Count > 0)
                {
                    (Scene key, SceneInstance value) = Instances.Entries.First();

                    this.Unload(value.Id, key);
                }
            }

            Instances.Clear();

            foreach ((GlobalId key, _) in Description.Loaders)
            {
                Loaders.Remove(key);
            }

            foreach ((GlobalId key, _) in Description.Scenes)
            {
                Scenes.Remove(key);
            }
        }

        public Scene Load(GlobalId id, ISceneLoadParameters parameters)
        {
            if (!id.IsValid()) throw new ArgumentException("Value should be valid.", nameof(id));
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            LogSceneLoad(id, parameters);

            Loading?.Invoke(id, parameters);

            Scene scene = OnLoad(id, parameters);
            SceneInstance instance = OnAddScene(id, scene, parameters);

            Instances.Add(scene, instance);

            Loaded?.Invoke(id, scene, parameters);

            LogSceneLoaded(id, scene, parameters);

            return scene;
        }

        public async Task<Scene> LoadAsync(GlobalId id, ISceneLoadParameters parameters)
        {
            if (!id.IsValid()) throw new ArgumentException("Value should be valid.", nameof(id));
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            LogSceneLoad(id, parameters, true);

            Loading?.Invoke(id, parameters);

            Scene scene = await OnLoadAsync(id, parameters);
            SceneInstance instance = OnAddScene(id, scene, parameters);

            Instances.Add(scene, instance);

            Loaded?.Invoke(id, scene, parameters);

            LogSceneLoaded(id, scene, parameters, true);

            return scene;
        }

        public void Unload(GlobalId id, Scene scene, ISceneUnloadParameters parameters)
        {
            if (!id.IsValid()) throw new ArgumentException("Value should be valid.", nameof(id));
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            LogSceneUnload(id, scene, parameters);

            Unloading?.Invoke(id, scene, parameters);

            OnRemoveScene(id, scene, parameters);
            OnUnload(id, scene, parameters);

            Instances.Remove(scene);

            Unloaded?.Invoke(id, parameters);

            LogSceneUnloaded(id, parameters);
        }

        public async Task UnloadAsync(GlobalId id, Scene scene, ISceneUnloadParameters parameters)
        {
            if (!id.IsValid()) throw new ArgumentException("Value should be valid.", nameof(id));
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            LogSceneUnload(id, scene, parameters, true);

            Unloading?.Invoke(id, scene, parameters);

            OnRemoveScene(id, scene, parameters);
            await OnUnloadAsync(id, scene, parameters);

            Instances.Remove(scene);

            Unloaded?.Invoke(id, parameters);

            LogSceneUnloaded(id, parameters, true);
        }

        protected virtual SceneInstance OnAddScene(GlobalId id, Scene scene, ISceneLoadParameters parameters)
        {
            if (Description.RegisterApplicationForScenes && ProviderInstance.TryGet(out IProvider<Scene, IApplication> provider) && !provider.Entries.ContainsKey(scene))
            {
                provider.Add(scene, Application);
            }

            return new SceneInstance(scene, id);
        }

        protected virtual void OnRemoveScene(GlobalId id, Scene scene, ISceneUnloadParameters parameters)
        {
            if (Description.RegisterApplicationForScenes && ProviderInstance.TryGet(out IProvider<Scene, IApplication> provider))
            {
                provider.Remove(scene);
            }
        }

        protected virtual Scene OnLoad(GlobalId id, ISceneLoadParameters parameters)
        {
            ISceneLoader loader = this.GetLoaderByScene(id);

            Scene scene = loader.Load(id, parameters, Context);

            return scene;
        }

        protected virtual Task<Scene> OnLoadAsync(GlobalId id, ISceneLoadParameters parameters)
        {
            ISceneLoader loader = this.GetLoaderByScene(id);

            Task<Scene> task = loader.LoadAsync(id, parameters, Context);

            return task;
        }

        protected virtual void OnUnload(GlobalId id, Scene scene, ISceneUnloadParameters parameters)
        {
            ISceneLoader loader = this.GetLoaderByScene(id);

            loader.Unload(id, scene, parameters, Context);
        }

        protected virtual Task OnUnloadAsync(GlobalId id, Scene scene, ISceneUnloadParameters parameters)
        {
            ISceneLoader loader = this.GetLoaderByScene(id);

            Task task = loader.UnloadAsync(id, scene, parameters, Context);

            return task;
        }
    }
}
