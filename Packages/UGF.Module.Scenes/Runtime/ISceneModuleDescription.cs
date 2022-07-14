using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.Ids;

namespace UGF.Module.Scenes.Runtime
{
    public interface ISceneModuleDescription : IApplicationModuleDescription
    {
        IReadOnlyDictionary<GlobalId, ISceneLoader> Loaders { get; }
        IReadOnlyDictionary<GlobalId, ISceneInfo> Scenes { get; }
        bool UnloadTrackedScenesOnUninitialize { get; }
    }
}
