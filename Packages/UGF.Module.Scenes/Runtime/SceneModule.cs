using System;
using System.Reflection;
using System.Threading.Tasks;
using UGF.Application.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime
{
    public class SceneModule : ApplicationModuleBase, ISceneModule
    {
        private static readonly Action<Scene, UnloadSceneOptions> m_unloadSceneInternal;

        static SceneModule()
        {
            MethodInfo methodInfo = typeof(SceneManager).GetMethod("UnloadSceneInternal", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static);

            if (methodInfo == null)
            {
                throw new Exception("SceneManager.UnloadSceneInternal method not found.");
            }

            m_unloadSceneInternal = (Action<Scene, UnloadSceneOptions>)methodInfo.CreateDelegate(typeof(Action<Scene, UnloadSceneOptions>));
        }

        public void LoadScene(string sceneName, LoadSceneParameters parameters)
        {
            if (string.IsNullOrEmpty(sceneName)) throw new ArgumentException("Value cannot be null or empty.", nameof(sceneName));

            SceneManager.LoadScene(sceneName, parameters);
        }

        public async Task<Scene> LoadSceneAsync(string sceneName, LoadSceneParameters parameters)
        {
            if (string.IsNullOrEmpty(sceneName)) throw new ArgumentException("Value cannot be null or empty.", nameof(sceneName));

            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, parameters);
            Scene scene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);

            while (operation.isDone)
            {
                await Task.Yield();
            }

            return scene;
        }

        public void UnloadScene(Scene scene, UnloadSceneOptions unloadOptions)
        {
            m_unloadSceneInternal(scene, unloadOptions);
        }

        public async Task UnloadSceneAsync(Scene scene, UnloadSceneOptions unloadOptions)
        {
            AsyncOperation operation = SceneManager.UnloadSceneAsync(scene, unloadOptions);

            while (operation.isDone)
            {
                await Task.Yield();
            }
        }
    }
}
