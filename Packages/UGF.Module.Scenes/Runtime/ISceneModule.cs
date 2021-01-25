using System.Threading.Tasks;
using UGF.Application.Runtime;
using UGF.RuntimeTools.Runtime.Providers;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime
{
    public interface ISceneModule : IApplicationModule
    {
        new ISceneModuleDescription Description { get; }
        IProvider<string, ISceneInfo> Scenes { get; }
        IProvider<string, ISceneLoader> Loaders { get; }
        IProvider<Scene, SceneInstance> Instances { get; }

        event SceneLoadHandler Loading;
        event SceneLoadedHandler Loaded;
        event SceneUnloadHandler Unloading;
        event SceneUnloadedHandler Unloaded;

        Scene Load(string id, SceneLoadParameters parameters);
        Task<Scene> LoadAsync(string id, SceneLoadParameters parameters);
        void Unload(string id, Scene scene, SceneUnloadParameters parameters);
        Task UnloadAsync(string id, Scene scene, SceneUnloadParameters parameters);
    }
}
