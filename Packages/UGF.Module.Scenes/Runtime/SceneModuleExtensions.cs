using System;

namespace UGF.Module.Scenes.Runtime
{
    public static class SceneModuleExtensions
    {
        public static ISceneLoader GetLoaderByScene(this ISceneModule sceneModule, string id)
        {
            return TryGetLoaderByScene(sceneModule, id, out ISceneLoader loader) ? loader : throw new ArgumentException($"Scene loader not found by the specified scene id: '{id}'.");
        }

        public static bool TryGetLoaderByScene(this ISceneModule sceneModule, string id, out ISceneLoader loader)
        {
            if (sceneModule == null) throw new ArgumentNullException(nameof(sceneModule));

            loader = default;
            return sceneModule.Scenes.TryGet(id, out ISceneInfo scene) && sceneModule.Loaders.TryGet(scene.LoaderId, out loader);
        }
    }
}
