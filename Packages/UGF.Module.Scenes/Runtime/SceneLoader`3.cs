using System.Threading.Tasks;
using UGF.EditorTools.Runtime.Ids;
using UGF.RuntimeTools.Runtime.Contexts;
using UGF.RuntimeTools.Runtime.Providers;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime
{
    public abstract class SceneLoader<TInfo, TLoadParameters, TUnloadParameters> : SceneLoaderBase
        where TInfo : class, ISceneInfo
        where TLoadParameters : class, ISceneLoadParameters
        where TUnloadParameters : class, ISceneUnloadParameters
    {
        protected SceneLoader(TLoadParameters defaultLoadParameters, TUnloadParameters defaultUnloadParameters) : base(defaultLoadParameters, defaultUnloadParameters)
        {
        }

        protected override Scene OnLoad(GlobalId id, ISceneLoadParameters parameters, IContext context)
        {
            var provider = context.Get<IProvider<GlobalId, ISceneInfo>>();
            var info = provider.Get<TInfo>(id);

            return OnLoad(id, info, (TLoadParameters)parameters, context);
        }

        protected override Task<Scene> OnLoadAsync(GlobalId id, ISceneLoadParameters parameters, IContext context)
        {
            var provider = context.Get<IProvider<GlobalId, ISceneInfo>>();
            var info = provider.Get<TInfo>(id);

            return OnLoadAsync(id, info, (TLoadParameters)parameters, context);
        }

        protected override void OnUnload(GlobalId id, Scene scene, ISceneUnloadParameters parameters, IContext context)
        {
            var provider = context.Get<IProvider<GlobalId, ISceneInfo>>();
            var info = provider.Get<TInfo>(id);

            OnUnload(id, scene, info, (TUnloadParameters)parameters, context);
        }

        protected override Task OnUnloadAsync(GlobalId id, Scene scene, ISceneUnloadParameters parameters, IContext context)
        {
            var provider = context.Get<IProvider<GlobalId, ISceneInfo>>();
            var info = provider.Get<TInfo>(id);

            return OnUnloadAsync(id, scene, info, (TUnloadParameters)parameters, context);
        }

        protected abstract Scene OnLoad(GlobalId id, TInfo info, TLoadParameters parameters, IContext context);
        protected abstract Task<Scene> OnLoadAsync(GlobalId id, TInfo info, TLoadParameters parameters, IContext context);
        protected abstract void OnUnload(GlobalId id, Scene scene, TInfo info, TUnloadParameters parameters, IContext context);
        protected abstract Task OnUnloadAsync(GlobalId id, Scene scene, TInfo info, TUnloadParameters parameters, IContext context);
    }
}
