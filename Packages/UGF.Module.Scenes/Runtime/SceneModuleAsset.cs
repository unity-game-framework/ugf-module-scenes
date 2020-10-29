using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.IMGUI.AssetReferences;
using UnityEngine;

namespace UGF.Module.Scenes.Runtime
{
    [CreateAssetMenu(menuName = "UGF/Scenes/Scene Module", order = 2000)]
    public class SceneModuleAsset : ApplicationModuleDescribedAsset<ISceneModule, SceneModuleDescription>
    {
        [SerializeField] private List<AssetReference<SceneLoaderAssetBase>> m_loaders = new List<AssetReference<SceneLoaderAssetBase>>();
        [SerializeField] private List<AssetReference<SceneInfoAssetBase>> m_scenes = new List<AssetReference<SceneInfoAssetBase>>();

        public List<AssetReference<SceneLoaderAssetBase>> Loaders { get { return m_loaders; } }
        public List<AssetReference<SceneInfoAssetBase>> Scenes { get { return m_scenes; } }

        protected override SceneModuleDescription OnGetDescription(IApplication application)
        {
            var description = new SceneModuleDescription();

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

        protected override ISceneModule OnBuild(IApplication application, SceneModuleDescription description)
        {
            return new SceneModule(application, description);
        }
    }
}
