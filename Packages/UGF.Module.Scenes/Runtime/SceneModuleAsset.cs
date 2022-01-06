using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.IMGUI.AssetReferences;
using UnityEngine;

namespace UGF.Module.Scenes.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Scenes/Scene Module", order = 2000)]
    public class SceneModuleAsset : ApplicationModuleAsset<ISceneModule, SceneModuleDescription>
    {
        [SerializeField] private bool m_unloadTrackedScenesOnUninitialize = true;
        [SerializeField] private bool m_registerApplicationForScenes = true;
        [SerializeField] private List<AssetReference<SceneLoaderAsset>> m_loaders = new List<AssetReference<SceneLoaderAsset>>();
        [SerializeField] private List<AssetReference<SceneGroupAsset>> m_groups = new List<AssetReference<SceneGroupAsset>>();

        public bool UnloadTrackedScenesOnUninitialize { get { return m_unloadTrackedScenesOnUninitialize; } set { m_unloadTrackedScenesOnUninitialize = value; } }
        public bool RegisterApplicationForScenes { get { return m_registerApplicationForScenes; } set { m_registerApplicationForScenes = value; } }
        public List<AssetReference<SceneLoaderAsset>> Loaders { get { return m_loaders; } }
        public List<AssetReference<SceneGroupAsset>> Groups { get { return m_groups; } }

        protected override IApplicationModuleDescription OnBuildDescription()
        {
            var description = new SceneModuleDescription
            {
                RegisterType = typeof(ISceneModule),
                UnloadTrackedScenesOnUninitialize = m_unloadTrackedScenesOnUninitialize,
                RegisterApplicationForScenes = m_registerApplicationForScenes
            };

            for (int i = 0; i < m_loaders.Count; i++)
            {
                AssetReference<SceneLoaderAsset> reference = m_loaders[i];
                ISceneLoader loader = reference.Asset.Build();

                description.Loaders.Add(reference.Guid, loader);
            }

            for (int i = 0; i < m_groups.Count; i++)
            {
                SceneGroupAsset group = m_groups[i].Asset;

                group.GetScenes(description.Scenes);
            }

            return description;
        }

        protected override ISceneModule OnBuild(SceneModuleDescription description, IApplication application)
        {
            return new SceneModule(description, application);
        }
    }
}
