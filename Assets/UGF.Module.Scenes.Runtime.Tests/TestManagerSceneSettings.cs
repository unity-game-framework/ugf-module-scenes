using NUnit.Framework;
using UGF.Module.Scenes.Runtime.Loaders.Manager;

namespace UGF.Module.Scenes.Runtime.Tests
{
    public class TestManagerSceneSettings
    {
        [TestCase("d39a9027b65879843ab7fc2c1a4a22af", ExpectedResult = "Assets/UGF.Module.Scenes.Runtime.Tests/Resources/SceneTest.unity")]
        public string GetScenePath(string id)
        {
            string path = ManagerSceneSettings.GetScenePath(id);

            return path;
        }

        [TestCase("Assets/UGF.Module.Scenes.Runtime.Tests/Resources/SceneTest.unity", ExpectedResult = "d39a9027b65879843ab7fc2c1a4a22af")]
        public string GetSceneId(string path)
        {
            string id = ManagerSceneSettings.GetSceneId(path);

            return id;
        }
    }
}
