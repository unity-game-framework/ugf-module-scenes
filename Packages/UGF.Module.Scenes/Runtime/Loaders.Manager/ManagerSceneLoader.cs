using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime.Loaders.Manager
{
    public partial class ManagerSceneLoader : SceneLoader<ManagerSceneInfo>
    {
        public bool UnloadUnusedAfterUnload { get; }

        public ManagerSceneLoader(bool unloadUnusedAfterUnload = true)
        {
            UnloadUnusedAfterUnload = unloadUnusedAfterUnload;
        }

        protected override Scene OnLoad(ISceneProvider provider, string id, ManagerSceneInfo info, SceneLoadParameters parameters)
        {
            LogSceneLoading(id, info, parameters);

            string scenePath = ManagerSceneSettings.GetScenePath(info.SceneId);
            var options = new LoadSceneParameters(parameters.AddMode, parameters.PhysicsMode);

            Scene scene = SceneManager.LoadScene(scenePath, options);

            LogSceneLoaded(id, info, parameters, scene);

            return scene;
        }

        protected override async Task<Scene> OnLoadAsync(ISceneProvider provider, string id, ManagerSceneInfo info, SceneLoadParameters parameters)
        {
            LogSceneLoading(id, info, parameters, true);

            string scenePath = ManagerSceneSettings.GetScenePath(info.SceneId);
            var options = new LoadSceneParameters(parameters.AddMode, parameters.PhysicsMode);

            AsyncOperation operation = SceneManager.LoadSceneAsync(scenePath, options);
            Scene scene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);

            operation.allowSceneActivation = parameters.AllowActivation;
            provider.OperationProvider.Add(scene, operation);

            if (operation.allowSceneActivation)
            {
                while (!operation.isDone)
                {
                    await Task.Yield();
                }

                provider.OperationProvider.Remove(scene);
            }
            else
            {
                while (operation.progress < 0.9F)
                {
                    await Task.Yield();
                }
            }

            LogSceneLoaded(id, info, parameters, scene, true);

            return scene;
        }

        protected override void OnUnload(ISceneProvider provider, string id, Scene scene, ManagerSceneInfo info, SceneUnloadParameters parameters)
        {
            LogSceneUnload(id, info, parameters, scene, UnloadUnusedAfterUnload);

            SceneUtility.UnloadScene(scene, parameters.Options);

            if (UnloadUnusedAfterUnload)
            {
                Resources.UnloadUnusedAssets();
            }

            LogSceneUnloaded(id, info, parameters, UnloadUnusedAfterUnload);
        }

        protected override async Task OnUnloadAsync(ISceneProvider provider, string id, Scene scene, ManagerSceneInfo info, SceneUnloadParameters parameters)
        {
            LogSceneUnload(id, info, parameters, scene, UnloadUnusedAfterUnload, true);

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

            LogSceneUnloaded(id, info, parameters, UnloadUnusedAfterUnload, true);
        }
    }
}
