using System.Threading.Tasks;
using UGF.RuntimeTools.Runtime.Contexts;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime
{
    public interface ISceneLoader
    {
        Scene Load(string id, SceneLoadParameters parameters, IContext context);
        Task<Scene> LoadAsync(string id, SceneLoadParameters parameters, IContext context);
        void Unload(string id, Scene scene, SceneUnloadParameters parameters, IContext context);
        Task UnloadAsync(string id, Scene scene, SceneUnloadParameters parameters, IContext context);
    }
}
