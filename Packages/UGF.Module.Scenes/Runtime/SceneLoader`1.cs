namespace UGF.Module.Scenes.Runtime
{
    public abstract class SceneLoader<TInfo> : SceneLoader<TInfo, ISceneLoadParameters, ISceneUnloadParameters> where TInfo : class, ISceneInfo
    {
    }
}
