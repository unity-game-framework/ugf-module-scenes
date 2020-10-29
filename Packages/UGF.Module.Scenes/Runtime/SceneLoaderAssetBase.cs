using UnityEngine;

namespace UGF.Module.Scenes.Runtime
{
    public abstract class SceneLoaderAssetBase : ScriptableObject
    {
        public T Build<T>() where T : class, ISceneLoader
        {
            return (T)OnBuild();
        }

        public ISceneLoader Build()
        {
            return OnBuild();
        }

        protected abstract ISceneLoader OnBuild();
    }
}
