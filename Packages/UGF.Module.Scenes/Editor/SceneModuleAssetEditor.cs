using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.AssetReferences;
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
        private AssetReferenceListDrawer m_listLoaders;
        private AssetReferenceListDrawer m_listGroups;

        private void OnEnable()
        {
            m_propertyUnloadTrackedScenesOnUninitialize = serializedObject.FindProperty("m_unloadTrackedScenesOnUninitialize");
            m_propertyRegisterApplicationForScenes = serializedObject.FindProperty("m_registerApplicationForScenes");

            m_listLoaders = new AssetReferenceListDrawer(serializedObject.FindProperty("m_loaders"));
            m_listGroups = new AssetReferenceListDrawer(serializedObject.FindProperty("m_groups"));

            m_listLoaders.Enable();
            m_listGroups.Enable();
        }

        private void OnDisable()
        {
            m_listLoaders.Disable();
            m_listGroups.Disable();
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
            }
        }
    }
}
