using System;
using UGF.EditorTools.Editor.Ids;
using UGF.EditorTools.Runtime.Ids;
using UnityEditor;

namespace UGF.Module.Scenes.Editor
{
    public static class SceneReferenceEditorUtility
    {
        public static void SetSceneToProperty(SerializedProperty serializedProperty, SceneAsset asset)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));
            if (asset == null) throw new ArgumentNullException(nameof(asset));

            string path = AssetDatabase.GetAssetPath(asset);
            SerializedProperty propertyGuid = serializedProperty.FindPropertyRelative("m_guid");
            SerializedProperty propertyPath = serializedProperty.FindPropertyRelative("m_path");

            propertyGuid.stringValue = AssetDatabase.AssetPathToGUID(path);
            propertyPath.stringValue = path;
        }

        public static bool TryGetSceneFromProperty(SerializedProperty serializedProperty, out SceneAsset asset)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            SerializedProperty propertyGuid = serializedProperty.FindPropertyRelative("m_guid");
            string guid = GlobalIdEditorUtility.GetGuidFromProperty(propertyGuid);
            string path = AssetDatabase.GUIDToAssetPath(guid);

            asset = AssetDatabase.LoadAssetAtPath<SceneAsset>(path);
            return asset != null;
        }

        public static bool TryUpdateScenePath(SerializedProperty serializedProperty, out string path)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            SerializedProperty propertyGuid = serializedProperty.FindPropertyRelative("m_guid");
            SerializedProperty propertyPath = serializedProperty.FindPropertyRelative("m_path");

            GlobalId guid = GlobalIdEditorUtility.GetGlobalIdFromProperty(propertyGuid);

            if (guid.IsValid())
            {
                path = AssetDatabase.GUIDToAssetPath(propertyGuid.stringValue);

                if (!string.IsNullOrEmpty(path))
                {
                    propertyPath.stringValue = path;
                    return true;
                }
            }

            path = default;
            return false;
        }
    }
}
