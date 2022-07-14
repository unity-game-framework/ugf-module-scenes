using System.Threading.Tasks;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.Ids;
using UGF.RuntimeTools.Runtime.Contexts;
using UGF.RuntimeTools.Runtime.Providers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime.Loaders.Manager
{
    public partial class ManagerSceneLoader : SceneLoader<ISceneInfo>
    {
        public bool RegisterApplication { get; set; } = true;
        public bool UnloadUnusedAfterUnload { get; set; } = true;

        public ManagerSceneLoader() : this(SceneLoadParameters.Default, SceneUnloadParameters.Default)
        {
        }

        public ManagerSceneLoader(ISceneLoadParameters defaultLoadParameters, ISceneUnloadParameters defaultUnloadParameters) : base(defaultLoadParameters, defaultUnloadParameters)
        {
        }

        protected override Scene OnLoad(GlobalId id, ISceneInfo info, ISceneLoadParameters parameters, IContext context)
        {
            LogSceneLoading(id, info, parameters);

            var options = new LoadSceneParameters(parameters.AddMode, parameters.PhysicsMode);

            Scene scene = SceneManager.LoadScene(info.Address, options);

            if (RegisterApplication)
            {
                OnRegisterApplication(scene, context);
            }

            LogSceneLoaded(id, info, parameters, scene);

            return scene;
        }

        protected override async Task<Scene> OnLoadAsync(GlobalId id, ISceneInfo info, ISceneLoadParameters parameters, IContext context)
        {
            LogSceneLoading(id, info, parameters, true);

            var provider = ProviderInstance.Get<IProvider<Scene, AsyncOperation>>();
            var options = new LoadSceneParameters(parameters.AddMode, parameters.PhysicsMode);

            AsyncOperation operation = SceneManager.LoadSceneAsync(info.Address, options);
            Scene scene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);

            if (RegisterApplication)
            {
                OnRegisterApplication(scene, context);
            }

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

        protected override void OnUnload(GlobalId id, Scene scene, ISceneInfo info, ISceneUnloadParameters parameters, IContext context)
        {
            LogSceneUnload(id, info, parameters, scene, UnloadUnusedAfterUnload);

            SceneUtility.UnloadScene(scene, parameters.Options);

            if (RegisterApplication)
            {
                OnUnregisterApplication(scene);
            }

            if (UnloadUnusedAfterUnload)
            {
                Resources.UnloadUnusedAssets();
            }

            LogSceneUnloaded(id, info, parameters, UnloadUnusedAfterUnload);
        }

        protected override async Task OnUnloadAsync(GlobalId id, Scene scene, ISceneInfo info, ISceneUnloadParameters parameters, IContext context)
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

            if (RegisterApplication)
            {
                OnUnregisterApplication(scene);
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

        protected virtual void OnRegisterApplication(Scene scene, IContext context)
        {
            if (ProviderInstance.TryGet(out IProvider<Scene, IApplication> provider) && !provider.Entries.ContainsKey(scene))
            {
                var application = context.Get<IApplication>();

                provider.Add(scene, application);
            }
        }

        protected virtual void OnUnregisterApplication(Scene scene)
        {
            if (ProviderInstance.TryGet(out IProvider<Scene, IApplication> provider))
            {
                provider.Remove(scene);
            }
        }
    }
}
