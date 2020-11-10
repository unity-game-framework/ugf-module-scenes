using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime.Loaders.Manager
{
    public class ManagerSceneLoader : SceneLoader<ManagerSceneInfo>
    {
        public bool UnloadUnusedAfterUnload { get; }

        public ManagerSceneLoader(bool unloadUnusedAfterUnload = true)
        {
            UnloadUnusedAfterUnload = unloadUnusedAfterUnload;
        }

        protected override Scene OnLoad(ISceneProvider provider, string id, ManagerSceneInfo info, SceneLoadParameters parameters)
        {
            string scenePath = info.SceneId;
            var options = new LoadSceneParameters(parameters.AddMode, parameters.PhysicsMode);

            Scene scene = SceneManager.LoadScene(scenePath, options);

            return scene;
        }

        protected override async Task<Scene> OnLoadAsync(ISceneProvider provider, string id, ManagerSceneInfo info, SceneLoadParameters parameters)
        {
            string scenePath = info.SceneId;
            var options = new LoadSceneParameters(parameters.AddMode, parameters.PhysicsMode);

            AsyncOperation operation = SceneManager.LoadSceneAsync(scenePath, options);
            Scene scene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);

            while (!operation.isDone)
            {
                await Task.Yield();
            }

            return scene;
        }

        protected override void OnUnload(ISceneProvider provider, string id, Scene scene, ManagerSceneInfo info, SceneUnloadParameters parameters)
        {
            SceneUtility.UnloadScene(scene, parameters.Options);

            if (UnloadUnusedAfterUnload)
            {
                Resources.UnloadUnusedAssets();
            }
        }

        protected override async Task OnUnloadAsync(ISceneProvider provider, string id, Scene scene, ManagerSceneInfo info, SceneUnloadParameters parameters)
        {
            AsyncOperation operation = SceneManager.UnloadSceneAsync(scene, parameters.Options);

            while (!operation.isDone)
            {
                await Task.Yield();
            }

            if (UnloadUnusedAfterUnload)
            {
                operation = Resources.UnloadUnusedAssets();

                while (!operation.isDone)
                {
                    await Task.Yield();
                }
            }
        }
    }
}
