using UGF.EditorTools.Runtime.IMGUI.Attributes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime.Loaders.Manager
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Scenes/Manager Scene Info", order = 2000)]
    public class ManagerSceneInfoAsset : SceneInfoAssetBase
    {
        [AssetGuid(typeof(SceneLoaderAsset))]
        [SerializeField] private string m_loader;
        [AssetGuid(typeof(Scene))]
        [SerializeField] private string m_scene;

        public string Loader { get { return m_loader; } set { m_loader = value; } }
        public string Scene { get { return m_scene; } set { m_scene = value; } }

        protected override ISceneInfo OnBuild()
        {
            return new ManagerSceneInfo(m_loader, m_scene);
        }
    }
}
