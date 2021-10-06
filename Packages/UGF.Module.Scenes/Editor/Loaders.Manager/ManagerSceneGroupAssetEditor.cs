using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.Module.Scenes.Runtime.Loaders.Manager;
using UnityEditor;
using UnityEngine;

namespace UGF.Module.Scenes.Editor.Loaders.Manager
{
    [CustomEditor(typeof(ManagerSceneGroupAsset), true)]
    internal class ManagerSceneGroupAssetEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyScript;
        private SerializedProperty m_propertyLoader;
        private ManagerSceneGroupAssetListDrawer m_listScenes;
        private Styles m_styles;

        private class Styles
        {
            public GUIContent RefreshContent { get; } = new GUIContent("Refresh", "Refresh all entries to update address for each entry.");
            public GUIContent RefreshAllContent { get; } = new GUIContent("Refresh All", "Refresh all groups in project to update address for each entry.");
            public string MissingEntryMessage { get; } = "Group contains entries with missing or invalid address.";
        }

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
            m_styles ??= new Styles();

            using (new SerializedObjectUpdateScope(serializedObject))
            {
                using (new EditorGUI.DisabledScope(true))
                {
                    EditorGUILayout.PropertyField(m_propertyScript);
                }

                EditorGUILayout.PropertyField(m_propertyLoader);

                m_listScenes.DrawGUILayout();
            }

            EditorGUILayout.Space();

            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.FlexibleSpace();

                if (GUILayout.Button(m_styles.RefreshAllContent))
                {
                    ManagerSceneEditorProgress.StartUpdateSceneGroupAll();
                }

                if (GUILayout.Button(m_styles.RefreshContent))
                {
                    ManagerSceneEditorUtility.UpdateSceneGroupEntries((ManagerSceneGroupAsset)target);
                    EditorUtility.SetDirty(target);
                }
            }

            EditorGUILayout.Space();

            if (ManagerSceneEditorUtility.IsSceneGroupHasMissingEntries((ManagerSceneGroupAsset)target))
            {
                EditorGUILayout.HelpBox(m_styles.MissingEntryMessage, MessageType.Warning);
            }
        }
    }
}
