using UGF.CustomSettings.Editor;
using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.Module.Scenes.Runtime.Loaders.Manager;
using UnityEditor;
using UnityEngine;

namespace UGF.Module.Scenes.Editor.Loaders.Manager
{
    [CustomEditor(typeof(ManagerSceneSettingsAsset), true)]
    internal class ManagerSceneSettingsAssetEditor : CustomSettingsDataEditor
    {
        private SerializedProperty m_propertyScenesAutoUpdate;
        private ReorderableListDrawer m_scenes;

        private void OnEnable()
        {
            m_propertyScenesAutoUpdate = serializedObject.FindProperty("m_scenesAutoUpdate");
            m_scenes = new ManagerSceneSettingsScenesListDrawer(serializedObject.FindProperty("m_scenes"));

            m_scenes.Enable();
        }

        private void OnDisable()
        {
            m_scenes.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorGUILayout.PropertyField(m_propertyScenesAutoUpdate);

                m_scenes.DrawGUILayout();
            }

            EditorGUILayout.Space();

            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.FlexibleSpace();

                if (GUILayout.Button("Update Scenes From Build Settings"))
                {
                    ManagerSceneEditorSettings.UpdateScenesFromBuildSettings();
                }
            }

            EditorGUILayout.Space();
            EditorGUILayout.HelpBox("The Scenes list represents runtime information about scenes in built player, and updated automatically after build settings scene list changed and when player built.", MessageType.Info);
        }
    }
}
