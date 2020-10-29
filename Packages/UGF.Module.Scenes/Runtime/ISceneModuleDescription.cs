using System.Collections.Generic;

namespace UGF.Module.Scenes.Runtime
{
    public interface ISceneModuleDescription
    {
        IReadOnlyDictionary<string, ISceneLoader> Loaders { get; }
        IReadOnlyDictionary<string, ISceneInfo> Scenes { get; }
    }
}
