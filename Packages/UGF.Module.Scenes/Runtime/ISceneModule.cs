using System.Threading.Tasks;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.Ids;
using UGF.RuntimeTools.Runtime.Contexts;
using UGF.RuntimeTools.Runtime.Providers;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime
{
    public interface ISceneModule : IApplicationModule
    {
        ISceneModuleDescription Description { get; }
        IProvider<GlobalId, ISceneLoader> Loaders { get; }
        IProvider<GlobalId, ISceneInfo> Scenes { get; }
        IProvider<Scene, SceneInstance> Instances { get; }
        IContext Context { get; }

        event SceneLoadHandler Loading;
        event SceneLoadedHandler Loaded;
        event SceneUnloadHandler Unloading;
        event SceneUnloadedHandler Unloaded;

        Scene Load(GlobalId id, ISceneLoadParameters parameters);
        Task<Scene> LoadAsync(GlobalId id, ISceneLoadParameters parameters);
        void Unload(GlobalId id, Scene scene, ISceneUnloadParameters parameters);
        Task UnloadAsync(GlobalId id, Scene scene, ISceneUnloadParameters parameters);
    }
}
