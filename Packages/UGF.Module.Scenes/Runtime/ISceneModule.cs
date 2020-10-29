using System.Collections.Generic;
using System.Threading.Tasks;
using UGF.Application.Runtime;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime
{
    public interface ISceneModule : IApplicationModuleDescribed
    {
        new ISceneModuleDescription Description { get; }
        ISceneProvider Provider { get; }
        IReadOnlyDictionary<Scene, SceneInstance> Scenes { get; }

        event SceneLoadHandler Loading;
        event SceneLoadedHandler Loaded;
        event SceneUnloadHandler Unloading;
        event SceneUnloadedHandler Unloaded;

        Scene Load(string id, SceneLoadParameters parameters);
        Task<Scene> LoadAsync(string id, SceneLoadParameters parameters);
        void Unload(string id, Scene scene, SceneUnloadParameters parameters);
        Task UnloadAsync(string id, Scene scene, SceneUnloadParameters parameters);
        SceneInstance GetScene(Scene scene);
        bool TryGetScene(Scene scene, out SceneInstance controller);
    }
}
