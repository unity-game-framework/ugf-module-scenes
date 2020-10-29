using System;
using System.Reflection;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime
{
    public static class SceneUtility
    {
        private static readonly Action<Scene, UnloadSceneOptions> m_unloadSceneInternal;

        static SceneUtility()
        {
            MethodInfo method = typeof(SceneManager).GetMethod("UnloadSceneInternal", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static)
                                ?? throw new Exception("SceneManager.UnloadSceneInternal method not found.");

            m_unloadSceneInternal = (Action<Scene, UnloadSceneOptions>)method.CreateDelegate(typeof(Action<Scene, UnloadSceneOptions>));
        }

        public static void UnloadScene(Scene scene, UnloadSceneOptions unloadOptions)
        {
            m_unloadSceneInternal.Invoke(scene, unloadOptions);
        }
    }
}
