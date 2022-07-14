using System;
using System.Threading.Tasks;
using UGF.EditorTools.Runtime.Ids;
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

        public Scene Load(GlobalId id, IContext context)
        {
            if (!id.IsValid()) throw new ArgumentException("Value should be valid.", nameof(id));
            if (context == null) throw new ArgumentNullException(nameof(context));

            return OnLoad(id, context);
        }

        public Scene Load(GlobalId id, ISceneLoadParameters parameters, IContext context)
        {
            if (!id.IsValid()) throw new ArgumentException("Value should be valid.", nameof(id));
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));
            if (context == null) throw new ArgumentNullException(nameof(context));

            return OnLoad(id, parameters, context);
        }

        public Task<Scene> LoadAsync(GlobalId id, IContext context)
        {
            if (!id.IsValid()) throw new ArgumentException("Value should be valid.", nameof(id));
            if (context == null) throw new ArgumentNullException(nameof(context));

            return OnLoadAsync(id, context);
        }

        public Task<Scene> LoadAsync(GlobalId id, ISceneLoadParameters parameters, IContext context)
        {
            if (!id.IsValid()) throw new ArgumentException("Value should be valid.", nameof(id));
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));
            if (context == null) throw new ArgumentNullException(nameof(context));

            return OnLoadAsync(id, parameters, context);
        }

        public void Unload(GlobalId id, Scene scene, IContext context)
        {
            if (!id.IsValid()) throw new ArgumentException("Value should be valid.", nameof(id));
            if (!scene.IsValid()) throw new ArgumentException("Value should be valid.", nameof(scene));
            if (context == null) throw new ArgumentNullException(nameof(context));

            OnUnload(id, scene, context);
        }

        public void Unload(GlobalId id, Scene scene, ISceneUnloadParameters parameters, IContext context)
        {
            if (!id.IsValid()) throw new ArgumentException("Value should be valid.", nameof(id));
            if (!scene.IsValid()) throw new ArgumentException("Value should be valid.", nameof(scene));
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));
            if (context == null) throw new ArgumentNullException(nameof(context));

            OnUnload(id, scene, parameters, context);
        }

        public Task UnloadAsync(GlobalId id, Scene scene, IContext context)
        {
            if (!id.IsValid()) throw new ArgumentException("Value should be valid.", nameof(id));
            if (!scene.IsValid()) throw new ArgumentException("Value should be valid.", nameof(scene));
            if (context == null) throw new ArgumentNullException(nameof(context));

            return OnUnloadAsync(id, scene, context);
        }

        public Task UnloadAsync(GlobalId id, Scene scene, ISceneUnloadParameters parameters, IContext context)
        {
            if (!id.IsValid()) throw new ArgumentException("Value should be valid.", nameof(id));
            if (!scene.IsValid()) throw new ArgumentException("Value should be valid.", nameof(scene));
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));
            if (context == null) throw new ArgumentNullException(nameof(context));

            return OnUnloadAsync(id, scene, parameters, context);
        }

        protected virtual Scene OnLoad(GlobalId id, IContext context)
        {
            return OnLoad(id, DefaultLoadParameters, context);
        }

        protected virtual Task<Scene> OnLoadAsync(GlobalId id, IContext context)
        {
            return OnLoadAsync(id, DefaultLoadParameters, context);
        }

        protected virtual void OnUnload(GlobalId id, Scene scene, IContext context)
        {
            OnUnload(id, scene, DefaultUnloadParameters, context);
        }

        protected virtual Task OnUnloadAsync(GlobalId id, Scene scene, IContext context)
        {
            return OnUnloadAsync(id, scene, DefaultUnloadParameters, context);
        }

        protected abstract Scene OnLoad(GlobalId id, ISceneLoadParameters parameters, IContext context);
        protected abstract Task<Scene> OnLoadAsync(GlobalId id, ISceneLoadParameters parameters, IContext context);
        protected abstract void OnUnload(GlobalId id, Scene scene, ISceneUnloadParameters parameters, IContext context);
        protected abstract Task OnUnloadAsync(GlobalId id, Scene scene, ISceneUnloadParameters parameters, IContext context);
    }
}
