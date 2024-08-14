using System.Collections.Generic;
using UGF.Description.Runtime;
using UGF.EditorTools.Runtime.Ids;

namespace UGF.Module.Scenes.Runtime
{
    public interface ISceneModuleDescription : IDescription
    {
        IReadOnlyDictionary<GlobalId, ISceneLoader> Loaders { get; }
        IReadOnlyDictionary<GlobalId, ISceneInfo> Scenes { get; }
        bool UnloadTrackedScenesOnUninitialize { get; }
        public bool RegisterApplicationForScenes { get; }
    }
}
