using UGF.Module.Scenes.Runtime;
using UnityEditor;

namespace UGF.Module.Scenes.Editor
{
    [CustomEditor(typeof(SceneDescription), true)]
    internal class SceneDescriptionEditor : UnityEditor.Editor
    {
        private readonly string[] m_exclude = { "m_Script", "m_name" };
        private SerializedProperty m_propertyScript;
        private SerializedProperty m_propertyName;
        private SerializedProperty m_propertyScene;

        private void OnEnable()
        {
            m_propertyScript = serializedObject.FindProperty("m_Script");
            m_propertyName = serializedObject.FindProperty("m_name");
            m_propertyScene = serializedObject.FindProperty("m_scene");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.UpdateIfRequiredOrScript();

            using (new EditorGUI.DisabledScope(true))
            {
                EditorGUILayout.PropertyField(m_propertyScript);
                EditorGUILayout.PropertyField(m_propertyName);
            }

            DrawPropertiesExcluding(serializedObject, m_exclude);
            UpdateNameFromAsset();

            serializedObject.ApplyModifiedProperties();
        }

        private void UpdateNameFromAsset()
        {
            string guid = m_propertyScene.stringValue;
            string path = AssetDatabase.GUIDToAssetPath(guid);
            var asset = AssetDatabase.LoadAssetAtPath<SceneAsset>(path);

            m_propertyName.stringValue = asset != null ? asset.name : string.Empty;
        }
    }
}
