using System.Collections.Generic;
using UGF.EditorTools.Runtime.Assets;
using UGF.EditorTools.Runtime.Ids;
using UnityEngine;

namespace UGF.Module.Scenes.Runtime.Loaders.Manager
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Scenes/Manager Scene Group", order = 2000)]
    public class ManagerSceneGroupAsset : SceneGroupAsset
    {
        [AssetId(typeof(SceneLoaderAsset))]
        [SerializeField] private Hash128 m_loader;
        [SerializeField] private List<SceneReference> m_scenes = new List<SceneReference>();

        public GlobalId Loader { get { return m_loader; } set { m_loader = value; } }
        public List<SceneReference> Scenes { get { return m_scenes; } }

        protected override void OnGetScenes(IDictionary<GlobalId, ISceneInfo> scenes)
        {
            for (int i = 0; i < m_scenes.Count; i++)
            {
                SceneReference reference = m_scenes[i];

                scenes.Add(reference.Guid, new SceneInfo(m_loader, reference.Path));
            }
        }
    }
}
