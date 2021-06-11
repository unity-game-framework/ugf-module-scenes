namespace UGF.Module.Scenes.Runtime
{
    public abstract class SceneLoader<TInfo> : SceneLoader<TInfo, ISceneLoadParameters, ISceneUnloadParameters> where TInfo : class, ISceneInfo
    {
        protected SceneLoader() : this(SceneLoadParameters.Default, SceneUnloadParameters.Default)
        {
        }

        protected SceneLoader(ISceneLoadParameters defaultLoadParameters, ISceneUnloadParameters defaultUnloadParameters) : base(defaultLoadParameters, defaultUnloadParameters)
        {
        }
    }
}
