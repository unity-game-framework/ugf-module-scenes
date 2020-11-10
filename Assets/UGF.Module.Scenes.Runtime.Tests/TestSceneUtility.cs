using System.Collections;
using JetBrains.Annotations;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace UGF.Module.Scenes.Runtime.Tests
{
    public class TestSceneUtility
    {
        [UnitySetUp, UnityTearDown, UsedImplicitly]
        public IEnumerator SetupAndTeardown()
        {
            EditorSceneManager.LoadSceneInPlayMode("Assets/Scenes/SampleScene.unity", new LoadSceneParameters(LoadSceneMode.Single));

            yield return null;
        }

        [Test]
        public void GetSceneGuid()
        {
            Scene scene = SceneManager.GetSceneAt(0);
            string guid = SceneUtility.GetSceneGuid(scene);

            Assert.NotNull(guid);
            Assert.AreEqual("9fc0d4010bbf28b4594072e72b8655ab", guid);
        }
    }
}
