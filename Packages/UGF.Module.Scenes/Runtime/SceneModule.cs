﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using UGF.Application.Runtime;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime
{
    public class SceneModule : ApplicationModuleDescribed<SceneModuleDescription>, ISceneModule
    {
        public ISceneProvider Provider { get; }
        public IReadOnlyDictionary<Scene, SceneInstance> Scenes { get; }

        ISceneModuleDescription ISceneModule.Description { get { return Description; } }

        public event SceneLoadHandler Loading;
        public event SceneLoadedHandler Loaded;
        public event SceneUnloadHandler Unloading;
        public event SceneUnloadedHandler Unloaded;

        private readonly Dictionary<Scene, SceneInstance> m_scenes = new Dictionary<Scene, SceneInstance>();

        public SceneModule(IApplication application, SceneModuleDescription description) : base(application, description)
        {
            Scenes = new ReadOnlyDictionary<Scene, SceneInstance>(m_scenes);
        }

        public Scene Load(string id, SceneLoadParameters parameters)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));

            Loading?.Invoke(id, parameters);

            Scene scene = OnLoad(id, parameters);

            Loaded?.Invoke(id, scene, parameters);

            return scene;
        }

        public async Task<Scene> LoadAsync(string id, SceneLoadParameters parameters)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));

            Loading?.Invoke(id, parameters);

            Scene scene = await OnLoadAsync(id, parameters);

            Loaded?.Invoke(id, scene, parameters);

            return scene;
        }

        public void Unload(string id, Scene scene, SceneUnloadParameters parameters)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));

            Unloading?.Invoke(id, scene, parameters);

            OnUnload(id, scene, parameters);

            Unloaded?.Invoke(id, parameters);
        }

        public async Task UnloadAsync(string id, Scene scene, SceneUnloadParameters parameters)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));

            Unloading?.Invoke(id, scene, parameters);

            await OnUnloadAsync(id, scene, parameters);

            Unloaded?.Invoke(id, parameters);
        }

        public SceneInstance GetScene(Scene scene)
        {
            return TryGetScene(scene, out SceneInstance instance) ? instance : throw new ArgumentException($"Scene instance not found by the specified scene: '{scene.name}'.");
        }

        public bool TryGetScene(Scene scene, out SceneInstance controller)
        {
            return m_scenes.TryGetValue(scene, out controller);
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
