using System;
using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.IMGUI.AssetReferences;
using UGF.EditorTools.Runtime.IMGUI.Attributes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Scenes/Scene Module", order = 2000)]
    public class SceneModuleAsset : ApplicationModuleAsset<ISceneModule, SceneModuleDescription>
    {
        [SerializeField] private bool m_unloadTrackedScenesOnUninitialize = true;
        [SerializeField] private bool m_registerApplicationForScenes = true;
        [SerializeField] private List<AssetReference<SceneLoaderAsset>> m_loaders = new List<AssetReference<SceneLoaderAsset>>();
        [SerializeField] private List<SceneEntry> m_scenes = new List<SceneEntry>();
        [SerializeField] private List<AssetReference<SceneGroupAsset>> m_groups = new List<AssetReference<SceneGroupAsset>>();

        public bool UnloadTrackedScenesOnUninitialize { get { return m_unloadTrackedScenesOnUninitialize; } set { m_unloadTrackedScenesOnUninitialize = value; } }
        public bool RegisterApplicationForScenes { get { return m_registerApplicationForScenes; } set { m_registerApplicationForScenes = value; } }
        public List<AssetReference<SceneLoaderAsset>> Loaders { get { return m_loaders; } }
        public List<SceneEntry> Scenes { get { return m_scenes; } }
        public List<AssetReference<SceneGroupAsset>> Groups { get { return m_groups; } }

        [Serializable]
        public struct SceneEntry
        {
            [AssetGuid(typeof(SceneLoaderAsset))]
            [SerializeField] private string m_loader;
            [AssetGuid(typeof(Scene))]
            [SerializeField] private string m_scene;

            public string Loader { get { return m_loader; } set { m_loader = value; } }
            public string Scene { get { return m_scene; } set { m_scene = value; } }
        }

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

            for (int i = 0; i < m_scenes.Count; i++)
            {
                SceneEntry scene = m_scenes[i];
                var info = new SceneInfo(scene.Loader, scene.Scene);

                description.Scenes.Add(scene.Scene, info);
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
