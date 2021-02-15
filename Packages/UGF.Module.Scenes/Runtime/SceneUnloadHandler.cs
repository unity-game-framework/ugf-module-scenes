using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime
{
    public delegate void SceneUnloadHandler(string id, Scene scene, ISceneUnloadParameters parameters);
}
