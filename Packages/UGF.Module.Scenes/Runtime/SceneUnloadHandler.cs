using UGF.EditorTools.Runtime.Ids;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime
{
    public delegate void SceneUnloadHandler(GlobalId id, Scene scene, ISceneUnloadParameters parameters);
}
