using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime
{
    public static class SceneModuleExtensions
    {
        public static ISceneLoadParameters GetDefaultLoadParametersByScene(this ISceneModule sceneModule, string id)
        {
            return TryGetDefaultLoadParametersByScene(sceneModule, id, out ISceneLoadParameters parameters) ? parameters : throw new ArgumentException($"Scene load parameters not found by the specified scene id: '{id}'.");
        }

        public static bool TryGetDefaultLoadParametersByScene(this ISceneModule sceneModule, string id, out ISceneLoadParameters parameters)
        {
            if (TryGetLoaderByScene(sceneModule, id, out ISceneLoader loader))
            {
                parameters = loader.DefaultLoadParameters;
                return true;
            }

            parameters = null;
            return false;
        }

        public static ISceneUnloadParameters GetDefaultUnloadParametersByScene(this ISceneModule sceneModule, string id)
        {
            return TryGetDefaultUnloadParametersByScene(sceneModule, id, out ISceneUnloadParameters parameters) ? parameters : throw new ArgumentException($"Scene unload parameters not found by the specified scene id: '{id}'.");
        }

        public static bool TryGetDefaultUnloadParametersByScene(this ISceneModule sceneModule, string id, out ISceneUnloadParameters parameters)
        {
            if (TryGetLoaderByScene(sceneModule, id, out ISceneLoader loader))
            {
                parameters = loader.DefaultUnloadParameters;
                return true;
            }

            parameters = null;
            return false;
        }

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

        public static Scene Load(this ISceneModule sceneModule, string id)
        {
            ISceneLoadParameters parameters = GetDefaultLoadParametersByScene(sceneModule, id);

            return sceneModule.Load(id, parameters);
        }

        public static Task<Scene> LoadAsync(this ISceneModule sceneModule, string id)
        {
            ISceneLoadParameters parameters = GetDefaultLoadParametersByScene(sceneModule, id);

            return sceneModule.LoadAsync(id, parameters);
        }

        public static void Unload(this ISceneModule sceneModule, string id, Scene scene)
        {
            ISceneUnloadParameters parameters = GetDefaultUnloadParametersByScene(sceneModule, id);

            sceneModule.Unload(id, scene, parameters);
        }

        public static Task UnloadAsync(this ISceneModule sceneModule, string id, Scene scene)
        {
            ISceneUnloadParameters parameters = GetDefaultUnloadParametersByScene(sceneModule, id);

            return sceneModule.UnloadAsync(id, scene, parameters);
        }
    }
}
