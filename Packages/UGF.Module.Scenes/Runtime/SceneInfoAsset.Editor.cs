#if UNITY_EDITOR
using UnityEditor;

namespace UGF.Module.Scenes.Runtime
{
    public partial class SceneInfoAsset
    {
        private void OnValidate()
        {
            m_path = AssetDatabase.GUIDToAssetPath(m_scene);
        }
    }
}
#endif
