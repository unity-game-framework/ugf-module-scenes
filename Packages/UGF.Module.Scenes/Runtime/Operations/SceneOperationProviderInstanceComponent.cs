using UGF.RuntimeTools.Runtime.Providers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime.Operations
{
    [DefaultExecutionOrder(int.MinValue)]
    [AddComponentMenu("Unity Game Framework/Scenes/Scene Operation Provider Instance", 2000)]
    public class SceneOperationProviderInstanceComponent : ProviderInstanceComponent<IProvider<Scene, AsyncOperation>>
    {
        protected override IProvider<Scene, AsyncOperation> OnCreateProvider()
        {
            return new Provider<Scene, AsyncOperation>();
        }
    }
}
