﻿using System;
using UGF.EditorTools.Runtime.Ids;
using UGF.Module.Scenes.Runtime;
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
            string guid = AssetDatabase.AssetPathToGUID(path);

            SerializedProperty propertyGuid = serializedProperty.FindPropertyRelative("m_guid");
            SerializedProperty propertyPath = serializedProperty.FindPropertyRelative("m_path");

            propertyGuid.hash128Value = GlobalId.TryParse(guid, out GlobalId id) ? id : default;
            propertyPath.stringValue = path;
        }

        public static bool TryGetSceneFromProperty(SerializedProperty serializedProperty, out SceneAsset asset)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            SerializedProperty propertyGuid = serializedProperty.FindPropertyRelative("m_guid");

            string guid = GlobalId.FromHash128(propertyGuid.hash128Value).ToString();
            string path = AssetDatabase.GUIDToAssetPath(guid);

            asset = AssetDatabase.LoadAssetAtPath<SceneAsset>(path);

            return asset != null;
        }

        public static bool TryUpdateScenePath(SceneReference reference, out string path)
        {
            path = AssetDatabase.GUIDToAssetPath(reference.Guid.ToString());

            return !string.IsNullOrEmpty(path);
        }

        public static void UpdateScenePathAll(SerializedProperty serializedProperty)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            for (int i = 0; i < serializedProperty.arraySize; i++)
            {
                SerializedProperty propertyElement = serializedProperty.GetArrayElementAtIndex(i);

                TryUpdateScenePath(propertyElement, out _);
            }
        }

        public static bool TryUpdateScenePath(SerializedProperty serializedProperty, out string path)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            SerializedProperty propertyGuid = serializedProperty.FindPropertyRelative("m_guid");
            SerializedProperty propertyPath = serializedProperty.FindPropertyRelative("m_path");

            GlobalId guid = propertyGuid.hash128Value;

            if (guid.IsValid())
            {
                path = AssetDatabase.GUIDToAssetPath(guid.ToString());

                if (!string.IsNullOrEmpty(path))
                {
                    propertyPath.stringValue = path;
                    return true;
                }
            }

            path = default;
            return false;
        }

        public static bool ValidateReferencesAll(SerializedProperty serializedProperty)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            for (int i = 0; i < serializedProperty.arraySize; i++)
            {
                SerializedProperty propertyElement = serializedProperty.GetArrayElementAtIndex(i);

                if (!ValidateReference(propertyElement))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool ValidateReference(SerializedProperty serializedProperty)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            return TryGetSceneFromProperty(serializedProperty, out _);
        }
    }
}
