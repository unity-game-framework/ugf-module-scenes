using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.Assets;
using UGF.EditorTools.Runtime.Ids;
using UnityEngine;

namespace UGF.Module.Scenes.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Scenes/Scene Module", order = 2000)]
    public class SceneModuleAsset : ApplicationModuleAsset<ISceneModule, SceneModuleDescription>
    {
        [SerializeField] private bool m_unloadTrackedScenesOnUninitialize = true;
        [SerializeField] private bool m_registerApplicationForScenes = true;
        [SerializeField] private List<AssetIdReference<SceneLoaderAsset>> m_loaders = new List<AssetIdReference<SceneLoaderAsset>>();
        [SerializeField] private List<AssetIdReference<SceneGroupAsset>> m_groups = new List<AssetIdReference<SceneGroupAsset>>();

        public bool UnloadTrackedScenesOnUninitialize { get { return m_unloadTrackedScenesOnUninitialize; } set { m_unloadTrackedScenesOnUninitialize = value; } }
        public bool RegisterApplicationForScenes { get { return m_registerApplicationForScenes; } set { m_registerApplicationForScenes = value; } }
        public List<AssetIdReference<SceneLoaderAsset>> Loaders { get { return m_loaders; } }
        public List<AssetIdReference<SceneGroupAsset>> Groups { get { return m_groups; } }

        protected override SceneModuleDescription OnBuildDescription()
        {
            var loaders = new Dictionary<GlobalId, ISceneLoader>();
            var scenes = new Dictionary<GlobalId, ISceneInfo>();

            for (int i = 0; i < m_loaders.Count; i++)
            {
                AssetIdReference<SceneLoaderAsset> reference = m_loaders[i];
                ISceneLoader loader = reference.Asset.Build();

                loaders.Add(reference.Guid, loader);
            }

            for (int i = 0; i < m_groups.Count; i++)
            {
                SceneGroupAsset group = m_groups[i].Asset;

                group.GetScenes(scenes);
            }

            return new SceneModuleDescription(loaders, scenes, m_unloadTrackedScenesOnUninitialize, m_registerApplicationForScenes);
        }

        protected override ISceneModule OnBuild(SceneModuleDescription description, IApplication application)
        {
            return new SceneModule(description, application);
        }
    }
}
