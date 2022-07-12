using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.Module.Scenes.Runtime;
using UnityEditor;

namespace UGF.Module.Scenes.Editor
{
    [CustomEditor(typeof(SceneModuleAsset), true)]
    internal class SceneModuleAssetEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyUnloadTrackedScenesOnUninitialize;
        private SerializedProperty m_propertyRegisterApplicationForScenes;
        private ReorderableListDrawer m_listLoaders;
        private ReorderableListSelectionDrawerByPath m_listLoadersSelection;
        private ReorderableListDrawer m_listGroups;
        private ReorderableListSelectionDrawerByPath m_listGroupsSelection;

        private void OnEnable()
        {
            m_propertyUnloadTrackedScenesOnUninitialize = serializedObject.FindProperty("m_unloadTrackedScenesOnUninitialize");
            m_propertyRegisterApplicationForScenes = serializedObject.FindProperty("m_registerApplicationForScenes");

            m_listLoaders = new ReorderableListDrawer(serializedObject.FindProperty("m_loaders"))
            {
                DisplayAsSingleLine = true
            };

            m_listLoadersSelection = new ReorderableListSelectionDrawerByPath(m_listLoaders, "m_asset")
            {
                Drawer =
                {
                    DisplayTitlebar = true
                }
            };

            m_listGroups = new ReorderableListDrawer(serializedObject.FindProperty("m_groups"))
            {
                DisplayAsSingleLine = true
            };

            m_listGroupsSelection = new ReorderableListSelectionDrawerByPath(m_listGroups, "m_asset")
            {
                Drawer =
                {
                    DisplayTitlebar = true
                }
            };

            m_listLoaders.Enable();
            m_listLoadersSelection.Enable();
            m_listGroups.Enable();
            m_listGroupsSelection.Enable();
        }

        private void OnDisable()
        {
            m_listLoaders.Disable();
            m_listLoadersSelection.Disable();
            m_listGroups.Disable();
            m_listGroupsSelection.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorIMGUIUtility.DrawScriptProperty(serializedObject);

                EditorGUILayout.PropertyField(m_propertyUnloadTrackedScenesOnUninitialize);
                EditorGUILayout.PropertyField(m_propertyRegisterApplicationForScenes);

                m_listLoaders.DrawGUILayout();
                m_listGroups.DrawGUILayout();

                m_listLoadersSelection.DrawGUILayout();
                m_listGroupsSelection.DrawGUILayout();
            }
        }
    }
}
