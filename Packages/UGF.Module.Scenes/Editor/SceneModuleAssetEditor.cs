using UGF.Application.Editor;
using UGF.EditorTools.Editor.IMGUI.AssetReferences;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.Module.Scenes.Runtime;
using UnityEditor;

namespace UGF.Module.Scenes.Editor
{
    [CustomEditor(typeof(SceneModuleAsset), true)]
    internal class SceneModuleAssetEditor : ApplicationModuleAssetEditor
    {
        private SerializedProperty m_propertyScript;
        private SerializedProperty m_propertyUnloadTrackedScenesOnUninitialize;
        private AssetReferenceListDrawer m_listLoaders;
        private AssetReferenceListDrawer m_listScenes;

        private void OnEnable()
        {
            m_propertyScript = serializedObject.FindProperty("m_Script");
            m_propertyUnloadTrackedScenesOnUninitialize = serializedObject.FindProperty("m_unloadTrackedScenesOnUninitialize");

            m_listLoaders = new AssetReferenceListDrawer(serializedObject.FindProperty("m_loaders"));
            m_listScenes = new AssetReferenceListDrawer(serializedObject.FindProperty("m_scenes"));

            m_listLoaders.Enable();
            m_listScenes.Enable();
        }

        private void OnDisable()
        {
            m_listLoaders.Disable();
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

                EditorGUILayout.PropertyField(m_propertyUnloadTrackedScenesOnUninitialize);

                m_listLoaders.DrawGUILayout();
                m_listScenes.DrawGUILayout();
            }

            DrawModuleRegisterTypeInfo();
        }
    }
}
