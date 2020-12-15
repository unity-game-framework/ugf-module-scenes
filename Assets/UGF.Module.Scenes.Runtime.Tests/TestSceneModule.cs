using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NUnit.Framework;
using UGF.Application.Runtime;
using UGF.Application.Runtime.Scenes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace UGF.Module.Scenes.Runtime.Tests
{
    public class TestSceneModule
    {
        [UnityTearDown, UsedImplicitly]
        public IEnumerator Teardown()
        {
            SceneManager.LoadScene("SampleScene", new LoadSceneParameters(LoadSceneMode.Single));
            ApplicationSceneProviderInstance.Provider.Clear();

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

            Assert.Contains(scene, module.Scenes.Keys.ToArray());
            Assert.Contains(scene, ApplicationSceneProviderInstance.Provider.Applications.Keys.ToArray());
            Assert.Contains(application, ApplicationSceneProviderInstance.Provider.Applications.Values.ToArray());

            yield return null;

            Assert.True(scene.IsValid(), "Load: scene.IsValid()");
            Assert.True(scene.isLoaded, "Load: scene.isLoaded");
            Assert.AreEqual(module.Provider.GetScene(id).Address, "d39a9027b65879843ab7fc2c1a4a22af");
            Assert.Contains(scene, module.Scenes.Keys.ToArray());

            if (unload)
            {
                module.Unload(id, scene, SceneUnloadParameters.Default);

                Assert.IsEmpty(module.Scenes.Keys);
                Assert.IsEmpty(ApplicationSceneProviderInstance.Provider.Applications.Keys);
                Assert.IsEmpty(ApplicationSceneProviderInstance.Provider.Applications.Values);

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

            Assert.Contains(scene, module.Scenes.Keys.ToArray());
            Assert.Contains(task.Result, ApplicationSceneProviderInstance.Provider.Applications.Keys.ToArray());
            Assert.Contains(application, ApplicationSceneProviderInstance.Provider.Applications.Values.ToArray());

            Assert.True(scene.IsValid(), "Load: scene.IsValid()");
            Assert.True(scene.isLoaded, "Load: scene.isLoaded");
            Assert.AreEqual(module.Provider.GetScene(id).Address, "d39a9027b65879843ab7fc2c1a4a22af");

            if (unload)
            {
                Task task2 = module.UnloadAsync(id, scene, SceneUnloadParameters.Default);

                while (!task2.IsCompleted)
                {
                    yield return null;
                }

                Assert.IsEmpty(module.Scenes.Keys);
                Assert.IsEmpty(ApplicationSceneProviderInstance.Provider.Applications.Keys);
                Assert.IsEmpty(ApplicationSceneProviderInstance.Provider.Applications.Values);

                Assert.False(scene.IsValid(), "Unload: scene.IsValid()");
                Assert.False(scene.isLoaded, "Unload: scene.isLoaded");
            }
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
