using System;
using System.Collections.Generic;
using UGF.EditorTools.Runtime.IMGUI.Attributes;
using UnityEngine;

namespace UGF.Module.Scenes.Runtime.Loaders.Manager
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Scenes/Manager Scene Group", order = 2000)]
    public class ManagerSceneGroupAsset : SceneGroupAsset
    {
        [AssetGuid(typeof(SceneLoaderAsset))]
        [SerializeField] private string m_loader;
        [SerializeField] private List<Entry> m_scenes = new List<Entry>();

        public string Loader { get { return m_loader; } set { m_loader = value; } }
        public List<Entry> Scenes { get { return m_scenes; } }

        [Serializable]
        public struct Entry
        {
            [SerializeField] private string m_id;
            [SerializeField] private string m_address;

            public string Id { get { return m_id; } set { m_id = value; } }
            public string Address { get { return m_address; } set { m_address = value; } }
        }

        protected override void OnGetScenes(IDictionary<string, ISceneInfo> scenes)
        {
            for (int i = 0; i < m_scenes.Count; i++)
            {
                Entry entry = m_scenes[i];
                var info = new SceneInfo(m_loader, entry.Address);

                scenes.Add(entry.Id, info);
            }
        }
    }
}
