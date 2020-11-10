using System;
using System.Collections.Generic;
using UGF.CustomSettings.Runtime;
using UnityEngine;

namespace UGF.Module.Scenes.Runtime.Loaders.Manager
{
    public class ManagerSceneSettingsAsset : CustomSettingsData
    {
        [SerializeField] private List<SceneData> m_scenes = new List<SceneData>();

        public List<SceneData> Scenes { get { return m_scenes; } }

        [Serializable]
        public struct SceneData
        {
            [SerializeField] private string m_id;
            [SerializeField] private string m_path;

            public string Id { get { return m_id; } set { m_id = value; } }
            public string Path { get { return m_path; } set { m_path = value; } }
        }
    }
}
