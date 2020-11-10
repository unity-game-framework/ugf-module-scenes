using System;
using System.Reflection;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime
{
    public static class SceneUtility
    {
        private static readonly Func<Scene, UnloadSceneOptions, bool> m_unloadSceneInternal;
        private static readonly Func<int, string> m_getGuidInternal;

        static SceneUtility()
        {
            MethodInfo unloadSceneInternal = typeof(SceneManager).GetMethod("UnloadSceneInternal", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static)
                                             ?? throw new ArgumentException("Method not found by the specified name: 'SceneManager.UnloadSceneInternal'.");

            MethodInfo getGuidInternal = typeof(Scene).GetMethod("GetGUIDInternal", BindingFlags.NonPublic | BindingFlags.Static)
                                         ?? throw new ArgumentException("Method not found by the specified name: 'Scene.GetGUIDInternal'.");

            m_unloadSceneInternal = (Func<Scene, UnloadSceneOptions, bool>)unloadSceneInternal.CreateDelegate(typeof(Func<Scene, UnloadSceneOptions, bool>));
            m_getGuidInternal = (Func<int, string>)getGuidInternal.CreateDelegate(typeof(Func<int, string>));
        }

        public static bool UnloadScene(Scene scene, UnloadSceneOptions unloadOptions)
        {
            if (!scene.IsValid()) throw new ArgumentException("Value should be valid.", nameof(scene));

            return m_unloadSceneInternal.Invoke(scene, unloadOptions);
        }

        public static string GetSceneGuid(Scene scene)
        {
            if (!scene.IsValid()) throw new ArgumentException("Value should be valid.", nameof(scene));

            return m_getGuidInternal.Invoke(scene.handle);
        }
    }
}
