using System;
using System.Reflection;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime
{
    public static class SceneUtility
    {
        private static readonly Func<Scene, UnloadSceneOptions, bool> m_unloadSceneInternal;

        static SceneUtility()
        {
            MethodInfo method = typeof(SceneManager).GetMethod("UnloadSceneInternal", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static)
                                ?? throw new Exception("SceneManager.UnloadSceneInternal method not found.");

            m_unloadSceneInternal = (Func<Scene, UnloadSceneOptions, bool>)method.CreateDelegate(typeof(Func<Scene, UnloadSceneOptions, bool>));
        }

        public static bool UnloadScene(Scene scene, UnloadSceneOptions unloadOptions)
        {
            if (!scene.IsValid()) throw new ArgumentException("Value should be valid.", nameof(scene));

            return m_unloadSceneInternal.Invoke(scene, unloadOptions);
        }
    }
}
