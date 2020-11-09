using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime.Loaders.Manager
{
    public class ManagerSceneLoader : SceneLoader<ISceneInfo>
    {
        public bool UnloadUnusedAfterUnload { get; }

        public ManagerSceneLoader(bool unloadUnusedAfterUnload = true)
        {
            UnloadUnusedAfterUnload = unloadUnusedAfterUnload;
        }

        protected override Scene OnLoad(ISceneProvider provider, string id, ISceneInfo info, SceneLoadParameters parameters)
        {
            var options = new LoadSceneParameters(parameters.AddMode, parameters.PhysicsMode);

            Scene scene = SceneManager.LoadScene(info.Address, options);

            return scene;
        }

        protected override async Task<Scene> OnLoadAsync(ISceneProvider provider, string id, ISceneInfo info, SceneLoadParameters parameters)
        {
            var options = new LoadSceneParameters(parameters.AddMode, parameters.PhysicsMode);

            AsyncOperation operation = SceneManager.LoadSceneAsync(info.Address, options);
            Scene scene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);

            while (!operation.isDone)
            {
                await Task.Yield();
            }

            return scene;
        }

        protected override void OnUnload(ISceneProvider provider, string id, Scene scene, ISceneInfo info, SceneUnloadParameters parameters)
        {
            SceneUtility.UnloadScene(scene, parameters.Options);

            if (UnloadUnusedAfterUnload)
            {
                Resources.UnloadUnusedAssets();
            }
        }

        protected override async Task OnUnloadAsync(ISceneProvider provider, string id, Scene scene, ISceneInfo info, SceneUnloadParameters parameters)
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
