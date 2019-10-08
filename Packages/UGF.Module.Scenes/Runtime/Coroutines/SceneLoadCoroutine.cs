using System;
using System.Collections;
using UGF.Coroutines.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime.Coroutines
{
    public class SceneLoadCoroutine : Coroutine<Scene>, ISceneLoadCoroutine
    {
        public string SceneName { get; }
        public SceneLoadParameters Parameters { get; }
        public bool AllowSceneActivation { get { return Parameters.Activate; } }

        private AsyncOperation m_operation;

        public SceneLoadCoroutine(string sceneName, SceneLoadParameters parameters)
        {
            if (string.IsNullOrEmpty(sceneName)) throw new ArgumentException("Value cannot be null or empty.", nameof(sceneName));

            SceneName = sceneName;
            Parameters = parameters;
        }

        public void ActivateScene()
        {
            if (!IsCompleted) throw new InvalidOperationException("Loading not yet completed.");

            m_operation.allowSceneActivation = true;
        }

        protected override IEnumerator Routine()
        {
            var parameters = new LoadSceneParameters(Parameters.Mode, Parameters.PhysicsMode);

            m_operation = SceneManager.LoadSceneAsync(SceneName, parameters);
            m_operation.allowSceneActivation = Parameters.Activate;

            Scene scene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);

            if (m_operation.allowSceneActivation)
            {
                yield return m_operation;
            }
            else
            {
                while (m_operation.progress < 0.9F)
                {
                    yield return null;
                }
            }

            Result = scene;

            if (Parameters.UnloadUnused)
            {
                yield return Resources.UnloadUnusedAssets();
            }
        }
    }
}
