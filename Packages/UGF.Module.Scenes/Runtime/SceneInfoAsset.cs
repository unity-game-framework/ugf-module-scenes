using UGF.EditorTools.Runtime.IMGUI.Attributes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Scenes/Scene Info", order = 2000)]
    public class SceneInfoAsset : SceneInfoAssetBase
    {
        [AssetGuid(typeof(SceneLoaderAsset))]
        [SerializeField] private string m_loader;
        [AssetPath(typeof(Scene))]
        [SerializeField] private string m_scene;

        public string Loader { get { return m_loader; } set { m_loader = value; } }
        public string Scene { get { return m_scene; } set { m_scene = value; } }

        protected override ISceneInfo OnBuild()
        {
            return new SceneInfo(m_loader, m_scene);
        }
    }
}
