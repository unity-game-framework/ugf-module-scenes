using UnityEngine;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime.Loaders.Manager
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Scenes/Manager Scene Loader", order = 2000)]
    public class ManagerSceneLoaderAsset : SceneLoaderAsset
    {
        [SerializeField] private bool m_unloadUnusedAfterUnload;
        [SerializeField] private SceneLoadParameters m_defaultLoadParameters = new SceneLoadParameters(LoadSceneMode.Single, LocalPhysicsMode.None);
        [SerializeField] private SceneUnloadParameters m_defaultUnloadParameters = new SceneUnloadParameters(UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);

        public bool UnloadUnusedAfterUnload { get { return m_unloadUnusedAfterUnload; } set { m_unloadUnusedAfterUnload = value; } }
        public SceneLoadParameters DefaultLoadParameters { get { return m_defaultLoadParameters; } }
        public SceneUnloadParameters DefaultUnloadParameters { get { return m_defaultUnloadParameters; } }

        protected override ISceneLoader OnBuild()
        {
            return new ManagerSceneLoader(m_defaultLoadParameters, m_defaultUnloadParameters, m_unloadUnusedAfterUnload);
        }
    }
}
