using System.Threading.Tasks;
using UGF.EditorTools.Runtime.Ids;
using UGF.RuntimeTools.Runtime.Contexts;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime
{
    public interface ISceneLoader
    {
        ISceneLoadParameters DefaultLoadParameters { get; }
        ISceneUnloadParameters DefaultUnloadParameters { get; }

        Scene Load(GlobalId id, IContext context);
        Scene Load(GlobalId id, ISceneLoadParameters parameters, IContext context);
        Task<Scene> LoadAsync(GlobalId id, IContext context);
        Task<Scene> LoadAsync(GlobalId id, ISceneLoadParameters parameters, IContext context);
        void Unload(GlobalId id, Scene scene, IContext context);
        void Unload(GlobalId id, Scene scene, ISceneUnloadParameters parameters, IContext context);
        Task UnloadAsync(GlobalId id, Scene scene, IContext context);
        Task UnloadAsync(GlobalId id, Scene scene, ISceneUnloadParameters parameters, IContext context);
    }
}
