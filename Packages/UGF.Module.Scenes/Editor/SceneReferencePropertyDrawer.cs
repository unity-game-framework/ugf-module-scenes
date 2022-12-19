using UGF.EditorTools.Editor.Ids;
using UGF.EditorTools.Editor.IMGUI.Attributes;
using UGF.EditorTools.Editor.IMGUI.PropertyDrawers;
using UGF.Module.Scenes.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Editor
{
    [CustomPropertyDrawer(typeof(SceneReference), true)]
    internal class SceneReferencePropertyDrawer : PropertyDrawerBase
    {
        protected override void OnDrawProperty(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            SerializedProperty propertyGuid = serializedProperty.FindPropertyRelative("m_guid");
            SerializedProperty propertyPath = serializedProperty.FindPropertyRelative("m_path");

            string guid = GlobalIdEditorUtility.GetGuidFromProperty(propertyGuid);

            guid = AttributeEditorGUIUtility.DrawAssetGuidField(position, guid, label, typeof(Scene));

            GlobalIdEditorUtility.SetGuidToProperty(propertyGuid, guid);

            propertyPath.stringValue = AssetDatabase.GUIDToAssetPath(guid);
        }

        public override float GetPropertyHeight(SerializedProperty serializedProperty, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }
    }
}
