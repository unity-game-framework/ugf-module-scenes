using System.Collections.Generic;

namespace UGF.Module.Scenes.Runtime
{
    public class SceneModuleDescription : ISceneModuleDescription
    {
        public Dictionary<string, ISceneLoader> Loaders { get; }
        public Dictionary<string, ISceneInfo> Scenes { get; }
        public bool UnloadTrackedScenesOnUninitialize { get; set; } = true;

        IReadOnlyDictionary<string, ISceneLoader> ISceneModuleDescription.Loaders { get { return Loaders; } }
        IReadOnlyDictionary<string, ISceneInfo> ISceneModuleDescription.Scenes { get { return Scenes; } }
    }
}
