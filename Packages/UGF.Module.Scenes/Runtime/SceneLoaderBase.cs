using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime
{
    public abstract class SceneLoaderBase : ISceneLoader
    {
        public Scene Load(ISceneProvider provider, string id, SceneLoadParameters parameters)
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));

            return OnLoad(provider, id, parameters);
        }

        public Task<Scene> LoadAsync(ISceneProvider provider, string id, SceneLoadParameters parameters)
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));

            return OnLoadAsync(provider, id, parameters);
        }

        public void Unload(ISceneProvider provider, string id, Scene scene, SceneUnloadParameters parameters)
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (!scene.IsValid()) throw new ArgumentException("Value should be valid.", nameof(scene));

            OnUnload(provider, id, scene, parameters);
        }

        public Task UnloadAsync(ISceneProvider provider, string id, Scene scene, SceneUnloadParameters parameters)
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (!scene.IsValid()) throw new ArgumentException("Value should be valid.", nameof(scene));

            return OnUnloadAsync(provider, id, scene, parameters);
        }

        protected abstract Scene OnLoad(ISceneProvider provider, string id, SceneLoadParameters parameters);
        protected abstract Task<Scene> OnLoadAsync(ISceneProvider provider, string id, SceneLoadParameters parameters);
        protected abstract void OnUnload(ISceneProvider provider, string id, Scene scene, SceneUnloadParameters parameters);
        protected abstract Task OnUnloadAsync(ISceneProvider provider, string id, Scene scene, SceneUnloadParameters parameters);
    }
}
