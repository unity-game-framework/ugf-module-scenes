using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.Module.Scenes.Runtime;
using UnityEditor;

namespace UGF.Module.Scenes.Editor
{
    [CustomEditor(typeof(SceneContainer), true)]
    internal class SceneContainerEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyScript;
        private ReorderableListDrawer m_list;

        private void OnEnable()
        {
            m_propertyScript = serializedObject.FindProperty("m_Script");
            m_list = new ReorderableListDrawer(serializedObject.FindProperty("m_containers"));

            m_list.Enable();
        }

        private void OnDisable()
        {
            m_list.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                using (new EditorGUI.DisabledScope(true))
                {
                    EditorGUILayout.PropertyField(m_propertyScript);
                }

                m_list.DrawGUILayout();
            }
        }
    }
}
