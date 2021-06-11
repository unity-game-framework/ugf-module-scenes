using System;
using System.Threading.Tasks;
using UGF.RuntimeTools.Runtime.Contexts;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime
{
    public abstract class SceneLoaderBase : ISceneLoader
    {
        public ISceneLoadParameters DefaultLoadParameters { get; }
        public ISceneUnloadParameters DefaultUnloadParameters { get; }

        protected SceneLoaderBase(ISceneLoadParameters defaultLoadParameters, ISceneUnloadParameters defaultUnloadParameters)
        {
            DefaultLoadParameters = defaultLoadParameters ?? throw new ArgumentNullException(nameof(defaultLoadParameters));
            DefaultUnloadParameters = defaultUnloadParameters ?? throw new ArgumentNullException(nameof(defaultUnloadParameters));
        }

        public Scene Load(string id, IContext context)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (context == null) throw new ArgumentNullException(nameof(context));

            return OnLoad(id, context);
        }

        public Scene Load(string id, ISceneLoadParameters parameters, IContext context)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));
            if (context == null) throw new ArgumentNullException(nameof(context));

            return OnLoad(id, parameters, context);
        }

        public Task<Scene> LoadAsync(string id, IContext context)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (context == null) throw new ArgumentNullException(nameof(context));

            return OnLoadAsync(id, context);
        }

        public Task<Scene> LoadAsync(string id, ISceneLoadParameters parameters, IContext context)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));
            if (context == null) throw new ArgumentNullException(nameof(context));

            return OnLoadAsync(id, parameters, context);
        }

        public void Unload(string id, Scene scene, IContext context)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (!scene.IsValid()) throw new ArgumentException("Value should be valid.", nameof(scene));
            if (context == null) throw new ArgumentNullException(nameof(context));

            OnUnload(id, scene, context);
        }

        public void Unload(string id, Scene scene, ISceneUnloadParameters parameters, IContext context)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (!scene.IsValid()) throw new ArgumentException("Value should be valid.", nameof(scene));
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));
            if (context == null) throw new ArgumentNullException(nameof(context));

            OnUnload(id, scene, parameters, context);
        }

        public Task UnloadAsync(string id, Scene scene, IContext context)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (!scene.IsValid()) throw new ArgumentException("Value should be valid.", nameof(scene));
            if (context == null) throw new ArgumentNullException(nameof(context));

            return OnUnloadAsync(id, scene, context);
        }

        public Task UnloadAsync(string id, Scene scene, ISceneUnloadParameters parameters, IContext context)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (!scene.IsValid()) throw new ArgumentException("Value should be valid.", nameof(scene));
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));
            if (context == null) throw new ArgumentNullException(nameof(context));

            return OnUnloadAsync(id, scene, parameters, context);
        }

        protected virtual Scene OnLoad(string id, IContext context)
        {
            return OnLoad(id, DefaultLoadParameters, context);
        }

        protected virtual Task<Scene> OnLoadAsync(string id, IContext context)
        {
            return OnLoadAsync(id, DefaultLoadParameters, context);
        }

        protected virtual void OnUnload(string id, Scene scene, IContext context)
        {
            OnUnload(id, scene, DefaultUnloadParameters, context);
        }

        protected virtual Task OnUnloadAsync(string id, Scene scene, IContext context)
        {
            return OnUnloadAsync(id, scene, DefaultUnloadParameters, context);
        }

        protected abstract Scene OnLoad(string id, ISceneLoadParameters parameters, IContext context);
        protected abstract Task<Scene> OnLoadAsync(string id, ISceneLoadParameters parameters, IContext context);
        protected abstract void OnUnload(string id, Scene scene, ISceneUnloadParameters parameters, IContext context);
        protected abstract Task OnUnloadAsync(string id, Scene scene, ISceneUnloadParameters parameters, IContext context);
    }
}
