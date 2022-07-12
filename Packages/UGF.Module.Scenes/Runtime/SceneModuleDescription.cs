using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.Ids;

namespace UGF.Module.Scenes.Runtime
{
    public class SceneModuleDescription : ApplicationModuleDescription, ISceneModuleDescription
    {
        public Dictionary<GlobalId, ISceneLoader> Loaders { get; } = new Dictionary<GlobalId, ISceneLoader>();
        public Dictionary<GlobalId, ISceneInfo> Scenes { get; } = new Dictionary<GlobalId, ISceneInfo>();
        public bool UnloadTrackedScenesOnUninitialize { get; set; } = true;
        public bool RegisterApplicationForScenes { get; set; } = true;

        IReadOnlyDictionary<GlobalId, ISceneLoader> ISceneModuleDescription.Loaders { get { return Loaders; } }
        IReadOnlyDictionary<GlobalId, ISceneInfo> ISceneModuleDescription.Scenes { get { return Scenes; } }
    }
}
