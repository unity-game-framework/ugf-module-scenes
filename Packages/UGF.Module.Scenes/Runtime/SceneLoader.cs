using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime
{
    public abstract class SceneLoader<TInfo> : SceneLoaderBase where TInfo : class, ISceneInfo
    {
        protected override Scene OnLoad(ISceneProvider provider, string id, SceneLoadParameters parameters)
        {
            var info = (TInfo)provider.GetScene(id);

            return OnLoad(provider, id, info, parameters);
        }

        protected override Task<Scene> OnLoadAsync(ISceneProvider provider, string id, SceneLoadParameters parameters)
        {
            var info = (TInfo)provider.GetScene(id);

            return OnLoadAsync(provider, id, info, parameters);
        }

        protected override void OnUnload(ISceneProvider provider, string id, Scene scene, SceneUnloadParameters parameters)
        {
            var info = (TInfo)provider.GetScene(id);

            OnUnload(provider, id, scene, info, parameters);
        }

        protected override Task OnUnloadAsync(ISceneProvider provider, string id, Scene scene, SceneUnloadParameters parameters)
        {
            var info = (TInfo)provider.GetScene(id);

            return OnUnloadAsync(provider, id, scene, info, parameters);
        }

        protected abstract Scene OnLoad(ISceneProvider provider, string id, TInfo info, SceneLoadParameters parameters);
        protected abstract Task<Scene> OnLoadAsync(ISceneProvider provider, string id, TInfo info, SceneLoadParameters parameters);
        protected abstract void OnUnload(ISceneProvider provider, string id, Scene scene, TInfo info, SceneUnloadParameters parameters);
        protected abstract Task OnUnloadAsync(ISceneProvider provider, string id, Scene scene, TInfo info, SceneUnloadParameters parameters);
    }
}
