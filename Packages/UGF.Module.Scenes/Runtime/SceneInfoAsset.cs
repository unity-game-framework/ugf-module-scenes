using UGF.EditorTools.Runtime.IMGUI.Attributes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime
{
    [CreateAssetMenu(menuName = "UGF/Scenes/Scene Info", order = 2000)]
    public class SceneInfoAsset : SceneInfoAssetBase
    {
        [AssetGuid(typeof(Scene))]
        [SerializeField] private string m_scene;
        [AssetGuid(typeof(SceneLoaderAssetBase))]
        [SerializeField] private string m_loader;
        [SerializeField] private string m_path;

        public string Scene { get { return m_scene; } set { m_scene = value; } }
        public string Loader { get { return m_loader; } set { m_loader = value; } }
        public string Path { get { return m_path; } set { m_path = value; } }

        protected override ISceneInfo OnBuild()
        {
            return new SceneInfo(m_loader, m_path);
        }
    }
}
