using UGF.EditorTools.Editor.IMGUI;
using UnityEditor;
using UnityEngine;

namespace UGF.Module.Scenes.Editor
{
    internal class SceneModuleAssetListDrawer : ReorderableListDrawer
    {
        public SceneModuleAssetListDrawer(SerializedProperty serializedProperty) : base(serializedProperty)
        {
        }

        protected override void OnDrawElementContent(Rect position, SerializedProperty serializedProperty, int index, bool isActive, bool isFocused)
        {
            SerializedProperty propertyLoader = serializedProperty.FindPropertyRelative("m_loader");
            SerializedProperty propertyScene = serializedProperty.FindPropertyRelative("m_scene");

            float space = EditorGUIUtility.standardVerticalSpacing;
            float indent = EditorIMGUIUtility.IndentPerLevel;

            var rectLoader = new Rect(position.x, position.y, EditorGUIUtility.labelWidth - space + indent, position.height);
            var rectScene = new Rect(rectLoader.xMax + space, position.y, position.width - rectLoader.width, position.height);

            EditorGUI.PropertyField(rectLoader, propertyLoader, GUIContent.none);
            EditorGUI.PropertyField(rectScene, propertyScene, GUIContent.none);
        }

        protected override float OnElementHeightContent(SerializedProperty serializedProperty, int index)
        {
            return EditorGUIUtility.singleLineHeight;
        }

        protected override bool OnElementHasVisibleChildren(SerializedProperty serializedProperty)
        {
            return false;
        }
    }
}
