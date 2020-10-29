using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime
{
    public interface ISceneLoader
    {
        Scene Load(ISceneProvider provider, string id, SceneLoadParameters parameters);
        Task<Scene> LoadAsync(ISceneProvider provider, string id, SceneLoadParameters parameters);
        void Unload(ISceneProvider provider, string id, Scene scene, SceneUnloadParameters parameters);
        Task UnloadAsync(ISceneProvider provider, string id, Scene scene, SceneUnloadParameters parameters);
    }
}
