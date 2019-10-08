using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Coroutine = UGF.Coroutines.Runtime.Coroutine;

namespace UGF.Module.Scenes.Runtime.Coroutines
{
    public class SceneUnloadCoroutine : Coroutine
    {
        public Scene Scene { get; }
        public SceneUnloadParameters Parameters { get; }

        public SceneUnloadCoroutine(Scene scene, SceneUnloadParameters parameters)
        {
            Scene = scene;
            Parameters = parameters;
        }

        protected override IEnumerator Routine()
        {
            AsyncOperation operation = SceneManager.UnloadSceneAsync(Scene, Parameters.UnloadOptions);

            yield return operation;

            if (Parameters.UnloadUnused)
            {
                yield return Resources.UnloadUnusedAssets();
            }
        }
    }
}
