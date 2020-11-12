using System.Diagnostics;
using UGF.Logs.Runtime;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime.Loaders.Manager
{
    public partial class ManagerSceneLoader
    {
        [Conditional("UNITY_EDITOR")]
        [Conditional(LogUtility.LOG_DEBUG_DEFINE)]
        private static void LogSceneLoading(string id, ManagerSceneInfo info, SceneLoadParameters parameters, bool isAsync = false)
        {
            Log.Debug("Manager Scene Loader loading", new
            {
                id,
                info = new
                {
                    info.LoaderId,
                    info.SceneId
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
        private static void LogSceneLoaded(string id, ManagerSceneInfo info, SceneLoadParameters parameters, Scene scene, bool isAsync = false)
        {
            Log.Debug("Manager Scene Loader loaded", new
            {
                id,
                info = new
                {
                    info.LoaderId,
                    info.SceneId
                },
                parameters = new
                {
                    parameters.AddMode,
                    parameters.PhysicsMode
                },
                scene = new
                {
                    scene.handle,
                    scene.name,
                    scene.path,
                    scene.buildIndex,
                    scene.rootCount,
                    scene.isSubScene
                },
                isAsync
            });
        }

        [Conditional("UNITY_EDITOR")]
        [Conditional(LogUtility.LOG_DEBUG_DEFINE)]
        private static void LogSceneUnload(string id, ManagerSceneInfo info, SceneUnloadParameters parameters, Scene scene, bool unloadUnused, bool isAsync = false)
        {
            Log.Debug("Manager Scene Loader unloading", new
            {
                id,
                info = new
                {
                    info.LoaderId,
                    info.SceneId
                },
                parameters = new
                {
                    parameters.Options
                },
                scene = new
                {
                    scene.handle,
                    scene.name,
                    scene.path,
                    scene.buildIndex,
                    scene.rootCount,
                    scene.isSubScene
                },
                unloadUnused,
                isAsync
            });
        }

        [Conditional("UNITY_EDITOR")]
        [Conditional(LogUtility.LOG_DEBUG_DEFINE)]
        private static void LogSceneUnloaded(string id, ManagerSceneInfo info, SceneUnloadParameters parameters, bool unloadUnused, bool isAsync = false)
        {
            Log.Debug("Manager Scene Loader unloaded", new
            {
                id,
                info = new
                {
                    info.LoaderId,
                    info.SceneId
                },
                parameters = new
                {
                    parameters.Options
                },
                unloadUnused,
                isAsync
            });
        }
    }
}
