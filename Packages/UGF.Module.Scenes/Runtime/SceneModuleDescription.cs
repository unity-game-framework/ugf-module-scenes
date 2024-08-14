using System;
using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.Ids;

namespace UGF.Module.Scenes.Runtime
{
    public class SceneModuleDescription : ApplicationModuleDescription, ISceneModuleDescription
    {
        public IReadOnlyDictionary<GlobalId, ISceneLoader> Loaders { get; }
        public IReadOnlyDictionary<GlobalId, ISceneInfo> Scenes { get; }
        public bool UnloadTrackedScenesOnUninitialize { get; }
        public bool RegisterApplicationForScenes { get; }

        public SceneModuleDescription(
            IReadOnlyDictionary<GlobalId, ISceneLoader> loaders,
            IReadOnlyDictionary<GlobalId, ISceneInfo> scenes,
            bool unloadTrackedScenesOnUninitialize,
            bool registerApplicationForScenes)
        {
            Loaders = loaders ?? throw new ArgumentNullException(nameof(loaders));
            Scenes = scenes ?? throw new ArgumentNullException(nameof(scenes));
            UnloadTrackedScenesOnUninitialize = unloadTrackedScenesOnUninitialize;
            RegisterApplicationForScenes = registerApplicationForScenes;
        }
    }
}
