using UnityEngine;

namespace UGF.Module.Scenes.Runtime
{
    public abstract class SceneInfoAssetBase : ScriptableObject
    {
        public T Build<T>() where T : class, ISceneInfo
        {
            return (T)OnBuild();
        }

        public ISceneInfo Build()
        {
            return OnBuild();
        }

        protected abstract ISceneInfo OnBuild();
    }
}
