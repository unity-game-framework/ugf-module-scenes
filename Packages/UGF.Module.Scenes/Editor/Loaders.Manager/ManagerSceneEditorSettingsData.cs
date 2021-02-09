using UGF.CustomSettings.Runtime;
using UnityEngine;

namespace UGF.Module.Scenes.Editor.Loaders.Manager
{
    public class ManagerSceneEditorSettingsData : CustomSettingsData
    {
        [SerializeField] private bool m_updateAllGroupsOnBuild = true;

        public bool UpdateAllGroupsOnBuild { get { return m_updateAllGroupsOnBuild; } set { m_updateAllGroupsOnBuild = value; } }
    }
}
