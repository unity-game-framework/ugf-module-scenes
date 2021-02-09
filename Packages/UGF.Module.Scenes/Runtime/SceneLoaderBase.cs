﻿using System;
using System.Threading.Tasks;
using UGF.RuntimeTools.Runtime.Contexts;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime
{
    public abstract class SceneLoaderBase : ISceneLoader
    {
        public Scene Load(string id, ISceneLoadParameters parameters, IContext context)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));
            if (context == null) throw new ArgumentNullException(nameof(context));

            return OnLoad(id, parameters, context);
        }

        public Task<Scene> LoadAsync(string id, ISceneLoadParameters parameters, IContext context)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));
            if (context == null) throw new ArgumentNullException(nameof(context));

            return OnLoadAsync(id, parameters, context);
        }

        public void Unload(string id, Scene scene, ISceneUnloadParameters parameters, IContext context)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (!scene.IsValid()) throw new ArgumentException("Value should be valid.", nameof(scene));
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));
            if (context == null) throw new ArgumentNullException(nameof(context));

            OnUnload(id, scene, parameters, context);
        }

        public Task UnloadAsync(string id, Scene scene, ISceneUnloadParameters parameters, IContext context)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (!scene.IsValid()) throw new ArgumentException("Value should be valid.", nameof(scene));
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));
            if (context == null) throw new ArgumentNullException(nameof(context));

            return OnUnloadAsync(id, scene, parameters, context);
        }

        protected abstract Scene OnLoad(string id, ISceneLoadParameters parameters, IContext context);
        protected abstract Task<Scene> OnLoadAsync(string id, ISceneLoadParameters parameters, IContext context);
        protected abstract void OnUnload(string id, Scene scene, ISceneUnloadParameters parameters, IContext context);
        protected abstract Task OnUnloadAsync(string id, Scene scene, ISceneUnloadParameters parameters, IContext context);
    }
}
