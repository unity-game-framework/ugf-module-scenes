﻿using System.Threading.Tasks;
using UGF.RuntimeTools.Runtime.Contexts;
using UGF.RuntimeTools.Runtime.Providers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime.Loaders.Manager
{
    public partial class ManagerSceneLoader : SceneLoader<ISceneInfo>
    {
        public bool UnloadUnusedAfterUnload { get; }

        public ManagerSceneLoader(bool unloadUnusedAfterUnload = true) : this(SceneLoadParameters.Default, SceneUnloadParameters.Default, unloadUnusedAfterUnload)
        {
            UnloadUnusedAfterUnload = unloadUnusedAfterUnload;
        }

        public ManagerSceneLoader(ISceneLoadParameters defaultLoadParameters, ISceneUnloadParameters defaultUnloadParameters, bool unloadUnusedAfterUnload) : base(defaultLoadParameters, defaultUnloadParameters)
        {
            UnloadUnusedAfterUnload = unloadUnusedAfterUnload;
        }

        protected override Scene OnLoad(string id, ISceneInfo info, ISceneLoadParameters parameters, IContext context)
        {
            LogSceneLoading(id, info, parameters);

            var options = new LoadSceneParameters(parameters.AddMode, parameters.PhysicsMode);

            Scene scene = SceneManager.LoadScene(info.Address, options);

            LogSceneLoaded(id, info, parameters, scene);

            return scene;
        }

        protected override async Task<Scene> OnLoadAsync(string id, ISceneInfo info, ISceneLoadParameters parameters, IContext context)
        {
            LogSceneLoading(id, info, parameters, true);

            var provider = ProviderInstance.Get<IProvider<Scene, AsyncOperation>>();
            var options = new LoadSceneParameters(parameters.AddMode, parameters.PhysicsMode);

            AsyncOperation operation = SceneManager.LoadSceneAsync(info.Address, options);
            Scene scene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);

            operation.allowSceneActivation = parameters.AllowActivation;
            provider.Add(scene, operation);

            if (operation.allowSceneActivation)
            {
                while (!operation.isDone)
                {
                    await Task.Yield();
                }

                provider.Remove(scene);
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

        protected override void OnUnload(string id, Scene scene, ISceneInfo info, ISceneUnloadParameters parameters, IContext context)
        {
            LogSceneUnload(id, info, parameters, scene, UnloadUnusedAfterUnload);

            SceneUtility.UnloadScene(scene, parameters.Options);

            if (UnloadUnusedAfterUnload)
            {
                Resources.UnloadUnusedAssets();
            }

            LogSceneUnloaded(id, info, parameters, UnloadUnusedAfterUnload);
        }

        protected override async Task OnUnloadAsync(string id, Scene scene, ISceneInfo info, ISceneUnloadParameters parameters, IContext context)
        {
            LogSceneUnload(id, info, parameters, scene, UnloadUnusedAfterUnload, true);

            var provider = ProviderInstance.Get<IProvider<Scene, AsyncOperation>>();

            AsyncOperation operation = SceneManager.UnloadSceneAsync(scene, parameters.Options);

            provider.Add(scene, operation);

            while (!operation.isDone)
            {
                await Task.Yield();
            }

            provider.Remove(scene);

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
