using UnityEngine;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime.Operations
{
    public static class SceneExtensions
    {
        public static void Activate(this Scene scene, bool clearOperation = true)
        {
            ISceneOperationProvider provider = SceneOperationProviderInstance.Provider;
            AsyncOperation operation = provider.Get(scene);

            operation.allowSceneActivation = true;

            if (clearOperation)
            {
                provider.Remove(scene);
            }
        }

        public static AsyncOperation GetOperation(this Scene scene)
        {
            return SceneOperationProviderInstance.Provider.Get(scene);
        }

        public static bool TryGetOperation(this Scene scene, out AsyncOperation operation)
        {
            return SceneOperationProviderInstance.Provider.TryGet(scene, out operation);
        }
    }
}
