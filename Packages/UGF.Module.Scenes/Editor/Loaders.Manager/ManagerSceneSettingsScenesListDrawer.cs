using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Attributes;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Editor.Loaders.Manager
{
    internal class ManagerSceneSettingsScenesListDrawer : ReorderableListDrawer
    {
        public ManagerSceneSettingsScenesListDrawer(SerializedProperty serializedProperty) : base(serializedProperty)
        {
            List.displayAdd = false;
            List.displayRemove = false;
            List.draggable = false;
            List.footerHeight = 0F;
        }

        protected override void OnDrawSize(Rect position)
        {
            using (new EditorGUI.DisabledScope(ManagerSceneEditorSettings.ScenesAutoUpdate))
            {
                base.OnDrawSize(position);
            }
        }

        protected override void OnDrawElementContent(Rect position, SerializedProperty serializedProperty, int index, bool isActive, bool isFocused)
        {
            SerializedProperty propertyId = serializedProperty.FindPropertyRelative("m_id");

            using (new EditorGUI.DisabledScope(ManagerSceneEditorSettings.ScenesAutoUpdate))
            {
                AttributeEditorGUIUtility.DrawAssetGuidField(position, propertyId, GUIContent.none, typeof(Scene));
            }
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
