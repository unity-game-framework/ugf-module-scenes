using UnityEngine;

namespace UGF.Module.Scenes.Runtime.Loaders.Manager
{
    [CreateAssetMenu(menuName = "UGF/Scenes/Manager Scene Loader", order = 2000)]
    public class ManagerSceneLoaderAsset : SceneLoaderAssetBase
    {
        [SerializeField] private bool m_unloadUnusedAfterUnload;

        public bool UnloadUnusedAfterUnload { get { return m_unloadUnusedAfterUnload; } set { m_unloadUnusedAfterUnload = value; } }

        protected override ISceneLoader OnBuild()
        {
            return new ManagerSceneLoader(m_unloadUnusedAfterUnload);
        }
    }
}
