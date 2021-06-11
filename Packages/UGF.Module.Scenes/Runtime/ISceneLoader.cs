using System.Threading.Tasks;
using UGF.RuntimeTools.Runtime.Contexts;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime
{
    public interface ISceneLoader
    {
        ISceneLoadParameters DefaultLoadParameters { get; }
        ISceneUnloadParameters DefaultUnloadParameters { get; }

        Scene Load(string id, IContext context);
        Scene Load(string id, ISceneLoadParameters parameters, IContext context);
        Task<Scene> LoadAsync(string id, IContext context);
        Task<Scene> LoadAsync(string id, ISceneLoadParameters parameters, IContext context);
        void Unload(string id, Scene scene, IContext context);
        void Unload(string id, Scene scene, ISceneUnloadParameters parameters, IContext context);
        Task UnloadAsync(string id, Scene scene, IContext context);
        Task UnloadAsync(string id, Scene scene, ISceneUnloadParameters parameters, IContext context);
    }
}
