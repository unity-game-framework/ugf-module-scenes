﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NUnit.Framework;
using UGF.Application.Runtime;
using UGF.Builder.Runtime;
using UGF.EditorTools.Runtime.Ids;
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

        [TestCase("Module", "d39a9027b65879843ab7fc2c1a4a22af", false, ExpectedResult = null)]
        [TestCase("Module", "d39a9027b65879843ab7fc2c1a4a22af", true, ExpectedResult = null)]
        [UnityTest]
        public IEnumerator Load(string moduleName, string value, bool unload)
        {
            var id = new GlobalId(value);
            IApplication application = CreateApplication(moduleName);

            application.Initialize();

            var module = application.GetModule<ISceneModule>();
            Scene scene = module.Load(id);

            Assert.Contains(scene, module.Instances.Entries.Keys.ToArray());
            Assert.Contains(scene, ProviderInstance.Get<IProvider<Scene, IApplication>>().Entries.Keys.ToArray());
            Assert.Contains(application, ProviderInstance.Get<IProvider<Scene, IApplication>>().Entries.Values.ToArray());

            yield return null;

            Assert.True(scene.IsValid(), "Load: scene.IsValid()");
            Assert.True(scene.isLoaded, "Load: scene.isLoaded");
            Assert.AreEqual(module.Scenes.Get(id).Address, "Assets/UGF.Module.Scenes.Runtime.Tests/Resources/SceneTest.unity");
            Assert.Contains(scene, module.Instances.Entries.Keys.ToArray());

            if (unload)
            {
                module.Unload(id, scene);

                Assert.IsEmpty(module.Instances.Entries.Keys);
                Assert.IsEmpty(ProviderInstance.Get<IProvider<Scene, IApplication>>().Entries.Keys);
                Assert.IsEmpty(ProviderInstance.Get<IProvider<Scene, IApplication>>().Entries.Values);

                yield return null;

                Assert.False(scene.IsValid(), "Unload: scene.IsValid()");
                Assert.False(scene.isLoaded, "Unload: scene.isLoaded");
            }
        }

        [TestCase("Module", "d39a9027b65879843ab7fc2c1a4a22af", false, ExpectedResult = null)]
        [TestCase("Module", "d39a9027b65879843ab7fc2c1a4a22af", true, ExpectedResult = null)]
        [UnityTest]
        public IEnumerator LoadAsync(string moduleName, string value, bool unload)
        {
            var id = new GlobalId(value);
            IApplication application = CreateApplication(moduleName);

            application.Initialize();

            var module = application.GetModule<ISceneModule>();
            Task<Scene> task = module.LoadAsync(id);

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
            Assert.AreEqual(module.Scenes.Get(id).Address, "Assets/UGF.Module.Scenes.Runtime.Tests/Resources/SceneTest.unity");

            if (unload)
            {
                Task task2 = module.UnloadAsync(id, scene);

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

        [TestCase("Module2", "d39a9027b65879843ab7fc2c1a4a22af", ExpectedResult = null)]
        [UnityTest]
        public IEnumerator LoadAsyncActivation(string moduleName, string value)
        {
            var id = new GlobalId(value);
            IApplication application = CreateApplication(moduleName);

            application.Initialize();

            var module = application.GetModule<ISceneModule>();
            Task<Scene> task = module.LoadAsync(id);

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
            Assert.AreEqual(module.Scenes.Get(id).Address, "Assets/UGF.Module.Scenes.Runtime.Tests/Resources/SceneTest.unity");

            scene.Activate();

            Assert.False(scene.TryGetOperation(out _));

            yield return operation;

            Assert.True(scene.IsValid(), "Load: scene.IsValid()");
            Assert.True(scene.isLoaded, "Load: scene.isLoaded");
        }

        private IApplication CreateApplication(string moduleName)
        {
            return new Application.Runtime.Application(new ApplicationDescription(false, new Dictionary<GlobalId, IBuilder<IApplication, IApplicationModule>>
            {
                { new GlobalId("4614ceca8914e5b4d8326f86aded3229"), Resources.Load<ApplicationModuleAsset>(moduleName) }
            }));
        }
    }
}
