using UGF.EditorTools.Editor.IMGUI.Attributes;
using UGF.EditorTools.Editor.IMGUI.PropertyDrawers;
using UGF.EditorTools.Runtime.Ids;
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

            propertyGuid.hash128Value = AttributeEditorGUIUtility.DrawAssetHash128Field(position, propertyGuid.hash128Value, label, typeof(Scene));
            propertyPath.stringValue = AssetDatabase.GUIDToAssetPath(GlobalId.FromHash128(propertyGuid.hash128Value).ToString());
        }

        public override float GetPropertyHeight(SerializedProperty serializedProperty, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }
    }
}
