using UnityEngine;

namespace UGF.Module.Scenes.Runtime.Loaders.Manager
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Scenes/Manager Scene Loader", order = 2000)]
    public class ManagerSceneLoaderAsset : SceneLoaderAsset
    {
        [SerializeField] private bool m_unloadUnusedAfterUnload;

        public bool UnloadUnusedAfterUnload { get { return m_unloadUnusedAfterUnload; } set { m_unloadUnusedAfterUnload = value; } }

        protected override ISceneLoader OnBuild()
        {
            return new ManagerSceneLoader(m_unloadUnusedAfterUnload);
        }
    }
}
