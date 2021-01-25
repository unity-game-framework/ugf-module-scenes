using System.Threading.Tasks;
using UGF.RuntimeTools.Runtime.Contexts;
using UGF.RuntimeTools.Runtime.Providers;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime
{
    public abstract class SceneLoader<TInfo> : SceneLoaderBase where TInfo : class, ISceneInfo
    {
        protected override Scene OnLoad(string id, SceneLoadParameters parameters, IContext context)
        {
            var provider = context.Get<IProvider<string, ISceneInfo>>();
            var info = provider.Get<TInfo>(id);

            return OnLoad(id, info, parameters, context);
        }

        protected override Task<Scene> OnLoadAsync(string id, SceneLoadParameters parameters, IContext context)
        {
            var provider = context.Get<IProvider<string, ISceneInfo>>();
            var info = provider.Get<TInfo>(id);

            return OnLoadAsync(id, info, parameters, context);
        }

        protected override void OnUnload(string id, Scene scene, SceneUnloadParameters parameters, IContext context)
        {
            var provider = context.Get<IProvider<string, ISceneInfo>>();
            var info = provider.Get<TInfo>(id);

            OnUnload(id, scene, info, parameters, context);
        }

        protected override Task OnUnloadAsync(string id, Scene scene, SceneUnloadParameters parameters, IContext context)
        {
            var provider = context.Get<IProvider<string, ISceneInfo>>();
            var info = provider.Get<TInfo>(id);

            return OnUnloadAsync(id, scene, info, parameters, context);
        }

        protected abstract Scene OnLoad(string id, TInfo info, SceneLoadParameters parameters, IContext context);
        protected abstract Task<Scene> OnLoadAsync(string id, TInfo info, SceneLoadParameters parameters, IContext context);
        protected abstract void OnUnload(string id, Scene scene, TInfo info, SceneUnloadParameters parameters, IContext context);
        protected abstract Task OnUnloadAsync(string id, Scene scene, TInfo info, SceneUnloadParameters parameters, IContext context);
    }
}
