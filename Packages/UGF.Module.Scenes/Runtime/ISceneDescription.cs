using System.Collections.Generic;
using UGF.Elements.Runtime;

namespace UGF.Module.Scenes.Runtime
{
    public interface ISceneDescription
    {
        string Name { get; }
        string AssetName { get; }
        SceneLoadParameters LoadParameters { get; }
        SceneUnloadParameters UnloadParameters { get; }
        IEnumerable<IElementBuilder> Elements { get; }
    }
}
