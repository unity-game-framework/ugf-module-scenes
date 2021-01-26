using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.Module.Scenes.Runtime.Loaders.Manager;
using UnityEditor;

namespace UGF.Module.Scenes.Editor.Loaders.Manager
{
    [CustomEditor(typeof(ManagerSceneGroupAsset), true)]
    internal class ManagerSceneGroupAssetEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyScript;
        private SerializedProperty m_propertyLoader;
        private ManagerSceneGroupAssetListDrawer m_listScenes;

        private void OnEnable()
        {
            m_propertyScript = serializedObject.FindProperty("m_Script");
            m_propertyLoader = serializedObject.FindProperty("m_loader");
            m_listScenes = new ManagerSceneGroupAssetListDrawer(serializedObject.FindProperty("m_scenes"));

            m_listScenes.Enable();
        }

        private void OnDisable()
        {
            m_listScenes.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                using (new EditorGUI.DisabledScope(true))
                {
                    EditorGUILayout.PropertyField(m_propertyScript);
                }

                EditorGUILayout.PropertyField(m_propertyLoader);

                m_listScenes.DrawGUILayout();
            }
        }
    }
}
