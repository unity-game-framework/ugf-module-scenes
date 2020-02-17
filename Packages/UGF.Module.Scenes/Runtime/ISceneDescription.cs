namespace UGF.Module.Scenes.Runtime
{
    public interface ISceneDescription
    {
        string Name { get; }
        string AssetName { get; }
        SceneLoadParameters LoadParameters { get; }
        SceneUnloadParameters UnloadParameters { get; }
    }
}
