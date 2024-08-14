using System.Threading.Tasks;
using UGF.RuntimeTools.Runtime.Providers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime.Operations
{
    public static class SceneExtensions
    {
        public static async Task ActivateAsync(this Scene scene)
        {
            scene.Activate();

            while (!scene.isLoaded)
            {
                await Task.Yield();
            }
        }

        public static void Activate(this Scene scene, bool clearOperation = true)
        {
            var provider = ProviderInstance.Get<IProvider<Scene, AsyncOperation>>();

            AsyncOperation operation = provider.Get(scene);

            operation.allowSceneActivation = true;

            if (clearOperation)
            {
                provider.Remove(scene);
            }
        }

        public static AsyncOperation GetOperation(this Scene scene)
        {
            return ProviderInstance.Get<IProvider<Scene, AsyncOperation>>().Get(scene);
        }

        public static bool TryGetOperation(this Scene scene, out AsyncOperation operation)
        {
            return ProviderInstance.Get<IProvider<Scene, AsyncOperation>>().TryGet(scene, out operation);
        }
    }
}
