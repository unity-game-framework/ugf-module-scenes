using System;
using System.Collections.Generic;
using UGF.Application.Runtime;

namespace UGF.Module.Scenes.Runtime
{
    public class SceneModuleDescription : ApplicationModuleDescription, ISceneModuleDescription
    {
        public Dictionary<string, ISceneLoader> Loaders { get; } = new Dictionary<string, ISceneLoader>();
        public Dictionary<string, ISceneInfo> Scenes { get; } = new Dictionary<string, ISceneInfo>();
        public bool UnloadTrackedScenesOnUninitialize { get; set; } = true;
        public bool RegisterApplicationForScenes { get; set; } = true;

        IReadOnlyDictionary<string, ISceneLoader> ISceneModuleDescription.Loaders { get { return Loaders; } }
        IReadOnlyDictionary<string, ISceneInfo> ISceneModuleDescription.Scenes { get { return Scenes; } }

        public SceneModuleDescription(Type registerType) : base(registerType)
        {
        }
    }
}
