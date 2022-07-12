using UGF.EditorTools.Editor.Ids;
using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Attributes;
using UnityEditor;
using UnityEngine;

namespace UGF.Module.Scenes.Editor.Loaders.Manager
{
    internal class ManagerSceneGroupAssetListDrawer : ReorderableListDrawer
    {
        public ManagerSceneGroupAssetListDrawer(SerializedProperty serializedProperty) : base(serializedProperty)
        {
        }

        protected override void OnDrawElementContent(Rect position, SerializedProperty serializedProperty, int index, bool isActive, bool isFocused)
        {
            SerializedProperty propertyId = serializedProperty.FindPropertyRelative("m_id");
            SerializedProperty propertyAddress = serializedProperty.FindPropertyRelative("m_address");

            AttributeEditorGUIUtility.DrawAssetPathField(position, propertyAddress, GUIContent.none, typeof(SceneAsset));

            var asset = AssetDatabase.LoadAssetAtPath<SceneAsset>(propertyAddress.stringValue);

            if (asset != null)
            {
                string path = AssetDatabase.GetAssetPath(asset);
                string guid = AssetDatabase.AssetPathToGUID(path);

                GlobalIdEditorUtility.SetGuidToProperty(propertyId, guid);
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
