using UGF.EditorTools.Runtime.Ids;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime
{
    public delegate void SceneLoadedHandler(GlobalId id, Scene scene, ISceneLoadParameters parameters);
}
