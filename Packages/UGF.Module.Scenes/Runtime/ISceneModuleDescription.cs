using System.Collections.Generic;
using UGF.Application.Runtime;

namespace UGF.Module.Scenes.Runtime
{
    public interface ISceneModuleDescription : IApplicationModuleDescription
    {
        IReadOnlyDictionary<string, ISceneLoader> Loaders { get; }
        IReadOnlyDictionary<string, ISceneInfo> Scenes { get; }
    }
}
