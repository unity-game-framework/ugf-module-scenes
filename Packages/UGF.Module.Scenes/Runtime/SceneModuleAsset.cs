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
        [SerializeField] private List<AssetReference<SceneLoaderAssetBase>> m_loaders = new List<AssetReference<SceneLoaderAssetBase>>();
        [SerializeField] private List<AssetReference<SceneInfoAssetBase>> m_scenes = new List<AssetReference<SceneInfoAssetBase>>();

        public bool UnloadTrackedScenesOnUninitialize { get { return m_unloadTrackedScenesOnUninitialize; } set { m_unloadTrackedScenesOnUninitialize = value; } }
        public List<AssetReference<SceneLoaderAssetBase>> Loaders { get { return m_loaders; } }
        public List<AssetReference<SceneInfoAssetBase>> Scenes { get { return m_scenes; } }

        protected override IApplicationModuleDescription OnBuildDescription()
        {
            var description = new SceneModuleDescription(typeof(ISceneModule))
            {
                UnloadTrackedScenesOnUninitialize = m_unloadTrackedScenesOnUninitialize
            };

            for (int i = 0; i < m_loaders.Count; i++)
            {
                AssetReference<SceneLoaderAssetBase> reference = m_loaders[i];
                ISceneLoader loader = reference.Asset.Build();

                description.Loaders.Add(reference.Guid, loader);
            }

            for (int i = 0; i < m_scenes.Count; i++)
            {
                AssetReference<SceneInfoAssetBase> reference = m_scenes[i];
                ISceneInfo scene = reference.Asset.Build();

                description.Scenes.Add(reference.Guid, scene);
            }

            return description;
        }

        protected override ISceneModule OnBuild(SceneModuleDescription description, IApplication application)
        {
            return new SceneModule(description, application);
        }
    }
}
