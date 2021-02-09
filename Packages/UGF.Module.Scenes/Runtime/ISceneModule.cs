using System.Threading.Tasks;
using UGF.Application.Runtime;
using UGF.RuntimeTools.Runtime.Contexts;
using UGF.RuntimeTools.Runtime.Providers;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime
{
    public interface ISceneModule : IApplicationModule
    {
        new ISceneModuleDescription Description { get; }
        IProvider<string, ISceneLoader> Loaders { get; }
        IProvider<string, ISceneInfo> Scenes { get; }
        IProvider<Scene, SceneInstance> Instances { get; }
        IContext Context { get; }

        event SceneLoadHandler Loading;
        event SceneLoadedHandler Loaded;
        event SceneUnloadHandler Unloading;
        event SceneUnloadedHandler Unloaded;

        Scene Load(string id, ISceneLoadParameters parameters);
        Task<Scene> LoadAsync(string id, ISceneLoadParameters parameters);
        void Unload(string id, Scene scene, ISceneUnloadParameters parameters);
        Task UnloadAsync(string id, Scene scene, ISceneUnloadParameters parameters);
    }
}
