using System.Diagnostics;
using UGF.Logs.Runtime;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime
{
    public partial class SceneModule
    {
        [Conditional("UNITY_EDITOR")]
        [Conditional(LogUtility.LOG_DEBUG_DEFINE)]
        private static void LogSceneLoad(string id, SceneLoadParameters parameters, bool isAsync = false)
        {
            Log.Debug("Scene loading", new
            {
                id,
                parameters = new
                {
                    parameters.AddMode,
                    parameters.PhysicsMode
                },
                isAsync
            });
        }

        [Conditional("UNITY_EDITOR")]
        [Conditional(LogUtility.LOG_DEBUG_DEFINE)]
        private static void LogSceneLoaded(string id, Scene scene, SceneLoadParameters parameters, bool isAsync = false)
        {
            Log.Debug("Scene loaded", new
            {
                id,
                scene = new
                {
                    scene.handle,
                    scene.name,
                    scene.path,
                    scene.buildIndex,
                    scene.rootCount,
                    scene.isSubScene
                },
                parameters = new
                {
                    parameters.AddMode,
                    parameters.PhysicsMode
                },
                isAsync
            });
        }

        [Conditional("UNITY_EDITOR")]
        [Conditional(LogUtility.LOG_DEBUG_DEFINE)]
        private static void LogSceneUnload(string id, Scene scene, SceneUnloadParameters parameters, bool isAsync = false)
        {
            Log.Debug("Scene unloading", new
            {
                id,
                scene = new
                {
                    scene.handle,
                    scene.name,
                    scene.path,
                    scene.buildIndex,
                    scene.rootCount,
                    scene.isSubScene
                },
                parameters = new
                {
                    parameters.Options
                },
                isAsync
            });
        }

        [Conditional("UNITY_EDITOR")]
        [Conditional(LogUtility.LOG_DEBUG_DEFINE)]
        private static void LogSceneUnloaded(string id, SceneUnloadParameters parameters, bool isAsync = false)
        {
            Log.Debug("Scene unloaded", new
            {
                id,
                parameters = new
                {
                    parameters.Options
                },
                isAsync
            });
        }
    }
}
