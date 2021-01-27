using System.Diagnostics;
using UGF.Logs.Runtime;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime.Loaders.Manager
{
    public partial class ManagerSceneLoader
    {
        [Conditional("UNITY_EDITOR")]
        [Conditional(LogUtility.LOG_DEBUG_DEFINE)]
        private static void LogSceneLoading(string id, ISceneInfo info, ISceneLoadParameters parameters, bool isAsync = false)
        {
            Log.Debug("Manager Scene Loader loading", new
            {
                id,
                info = new
                {
                    info.LoaderId,
                    info.Address
                },
                parameters = new
                {
                    parameters.AddMode,
                    parameters.PhysicsMode,
                    parameters.AllowActivation
                },
                isAsync
            });
        }

        [Conditional("UNITY_EDITOR")]
        [Conditional(LogUtility.LOG_DEBUG_DEFINE)]
        private static void LogSceneLoaded(string id, ISceneInfo info, ISceneLoadParameters parameters, Scene scene, bool isAsync = false)
        {
            Log.Debug("Manager Scene Loader loaded", new
            {
                id,
                info = new
                {
                    info.LoaderId,
                    info.Address
                },
                parameters = new
                {
                    parameters.AddMode,
                    parameters.PhysicsMode,
                    parameters.AllowActivation
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
        private static void LogSceneUnload(string id, ISceneInfo info, ISceneUnloadParameters parameters, Scene scene, bool unloadUnused, bool isAsync = false)
        {
            Log.Debug("Manager Scene Loader unloading", new
            {
                id,
                info = new
                {
                    info.LoaderId,
                    info.Address
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
        private static void LogSceneUnloaded(string id, ISceneInfo info, ISceneUnloadParameters parameters, bool unloadUnused, bool isAsync = false)
        {
            Log.Debug("Manager Scene Loader unloaded", new
            {
                id,
                info = new
                {
                    info.LoaderId,
                    info.Address
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
