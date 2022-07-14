using UGF.EditorTools.Runtime.Ids;

namespace UGF.Module.Scenes.Runtime
{
    public interface ISceneInfo
    {
        GlobalId LoaderId { get; }
        string Address { get; }
    }
}
