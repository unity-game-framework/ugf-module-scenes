using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NUnit.Framework;
using UGF.Application.Runtime;
using UGF.Module.Scenes.Runtime.Operations;
using UGF.RuntimeTools.Runtime.Providers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace UGF.Module.Scenes.Runtime.Tests
{
    public class TestSceneModule
    {
        [OneTimeSetUp]
        public void SetupOnce()
        {
            ProviderInstance.Set<IProvider<Scene, IApplication>>(new Provider<Scene, IApplication>());
            ProviderInstance.Set<IProvider<Scene, AsyncOperation>>(new Provider<Scene, AsyncOperation>());
        }

        [UnityTearDown, UsedImplicitly]
        public IEnumerator Teardown()
        {
            SceneManager.LoadScene("SampleScene", new LoadSceneParameters(LoadSceneMode.Single));

            if (ProviderInstance.TryGet(out IProvider<Scene, IApplication> provider))
            {
                provider.Clear();
            }

            yield return null;
        }

        [TestCase("Module", "70128677f5fe91241866ef846e7bb16e", false, ExpectedResult = null)]
        [TestCase("Module", "70128677f5fe91241866ef846e7bb16e", true, ExpectedResult = null)]
        [UnityTest]
        public IEnumerator Load(string moduleName, string id, bool unload)
        {
            IApplication application = CreateApplication(moduleName);

            application.Initialize();

            var module = application.GetModule<ISceneModule>();
            Scene scene = module.Load(id, SceneLoadParameters.DefaultAdditive);

            Assert.Contains(scene, module.Instances.Entries.Keys.ToArray());
            Assert.Contains(scene, ProviderInstance.Get<IProvider<Scene, IApplication>>().Entries.Keys.ToArray());
            Assert.Contains(application, ProviderInstance.Get<IProvider<Scene, IApplication>>().Entries.Values.ToArray());

            yield return null;

            Assert.True(scene.IsValid(), "Load: scene.IsValid()");
            Assert.True(scene.isLoaded, "Load: scene.isLoaded");
            Assert.AreEqual(module.Scenes.Get(id).Address, "d39a9027b65879843ab7fc2c1a4a22af");
            Assert.Contains(scene, module.Instances.Entries.Keys.ToArray());

            if (unload)
            {
                module.Unload(id, scene, SceneUnloadParameters.Default);

                Assert.IsEmpty(module.Instances.Entries.Keys);
                Assert.IsEmpty(ProviderInstance.Get<IProvider<Scene, IApplication>>().Entries.Keys);
                Assert.IsEmpty(ProviderInstance.Get<IProvider<Scene, IApplication>>().Entries.Values);

                yield return null;

                Assert.False(scene.IsValid(), "Unload: scene.IsValid()");
                Assert.False(scene.isLoaded, "Unload: scene.isLoaded");
            }
        }

        [TestCase("Module", "70128677f5fe91241866ef846e7bb16e", false, ExpectedResult = null)]
        [TestCase("Module", "70128677f5fe91241866ef846e7bb16e", true, ExpectedResult = null)]
        [UnityTest]
        public IEnumerator LoadAsync(string moduleName, string id, bool unload)
        {
            IApplication application = CreateApplication(moduleName);

            application.Initialize();

            var module = application.GetModule<ISceneModule>();
            Task<Scene> task = module.LoadAsync(id, SceneLoadParameters.DefaultAdditive);

            while (!task.IsCompleted)
            {
                yield return null;
            }

            Scene scene = task.Result;

            Assert.Contains(scene, module.Instances.Entries.Keys.ToArray());
            Assert.Contains(task.Result, ProviderInstance.Get<IProvider<Scene, IApplication>>().Entries.Keys.ToArray());
            Assert.Contains(application, ProviderInstance.Get<IProvider<Scene, IApplication>>().Entries.Values.ToArray());

            Assert.True(scene.IsValid(), "Load: scene.IsValid()");
            Assert.True(scene.isLoaded, "Load: scene.isLoaded");
            Assert.AreEqual(module.Scenes.Get(id).Address, "d39a9027b65879843ab7fc2c1a4a22af");

            if (unload)
            {
                Task task2 = module.UnloadAsync(id, scene, SceneUnloadParameters.Default);

                while (!task2.IsCompleted)
                {
                    yield return null;
                }

                Assert.IsEmpty(module.Instances.Entries.Keys);
                Assert.IsEmpty(ProviderInstance.Get<IProvider<Scene, IApplication>>().Entries.Keys);
                Assert.IsEmpty(ProviderInstance.Get<IProvider<Scene, IApplication>>().Entries.Values);

                Assert.False(scene.IsValid(), "Unload: scene.IsValid()");
                Assert.False(scene.isLoaded, "Unload: scene.isLoaded");
            }
        }

        [TestCase("Module", "70128677f5fe91241866ef846e7bb16e", ExpectedResult = null)]
        [UnityTest]
        public IEnumerator LoadAsyncActivation(string moduleName, string id)
        {
            IApplication application = CreateApplication(moduleName);

            application.Initialize();

            var module = application.GetModule<ISceneModule>();
            Task<Scene> task = module.LoadAsync(id, SceneLoadParameters.DefaultAdditiveDisabled);

            while (!task.IsCompleted)
            {
                yield return null;
            }

            Scene scene = task.Result;
            AsyncOperation operation = scene.GetOperation();

            Assert.True(scene.IsValid(), "Load: scene.IsValid()");
            Assert.False(scene.isLoaded, "Load: scene.isLoaded");
            Assert.NotNull(operation);
            Assert.GreaterOrEqual(operation.progress, 0.9F);
            Assert.AreEqual(module.Scenes.Get(id).Address, "d39a9027b65879843ab7fc2c1a4a22af");

            scene.Activate();

            Assert.False(scene.TryGetOperation(out _));

            yield return operation;

            Assert.True(scene.IsValid(), "Load: scene.IsValid()");
            Assert.True(scene.isLoaded, "Load: scene.isLoaded");
        }

        private IApplication CreateApplication(string moduleName)
        {
            return new ApplicationConfigured(new ApplicationResources
            {
                new ApplicationConfig
                {
                    Modules =
                    {
                        (IApplicationModuleBuilder)Resources.Load(moduleName, typeof(IApplicationModuleBuilder))
                    }
                }
            });
        }
    }
}
