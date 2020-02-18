using System.Threading.Tasks;
using UGF.Elements.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime
{
    public class SceneModule : SceneModuleBase
    {
        public SceneModule(IElementContext context) : base(context)
        {
        }

        protected override Scene OnLoad(string sceneName, SceneLoadParameters parameters)
        {
            var loadSceneParameters = new LoadSceneParameters
            {
                loadSceneMode = parameters.AddMode,
                localPhysicsMode = parameters.PhysicsMode
            };

            return SceneManager.LoadScene(sceneName, loadSceneParameters);
        }

        protected override async Task<Scene> OnLoadAsync(string sceneName, SceneLoadParameters parameters)
        {
            var loadSceneParameters = new LoadSceneParameters
            {
                loadSceneMode = parameters.AddMode,
                localPhysicsMode = parameters.PhysicsMode
            };

            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, loadSceneParameters);
            Scene scene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);

            while (operation.isDone)
            {
                await Task.Yield();
            }

            return scene;
        }

        protected override void OnUnload(Scene scene, SceneUnloadParameters parameters)
        {
            UnloadSceneOptions options = parameters.UnloadAllEmbeddedSceneObjects
                ? UnloadSceneOptions.UnloadAllEmbeddedSceneObjects
                : UnloadSceneOptions.None;

            SceneModuleUtility.UnloadScene(scene, options);
        }

        protected override async Task OnUnloadAsync(Scene scene, SceneUnloadParameters parameters)
        {
            UnloadSceneOptions options = parameters.UnloadAllEmbeddedSceneObjects
                ? UnloadSceneOptions.UnloadAllEmbeddedSceneObjects
                : UnloadSceneOptions.None;

            AsyncOperation operation = SceneManager.UnloadSceneAsync(scene, options);

            while (operation.isDone)
            {
                await Task.Yield();
            }
        }
    }
}
