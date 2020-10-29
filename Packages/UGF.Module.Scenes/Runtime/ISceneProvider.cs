using System.Collections.Generic;

namespace UGF.Module.Scenes.Runtime
{
    public interface ISceneProvider
    {
        IReadOnlyDictionary<string, ISceneLoader> Loaders { get; }
        IReadOnlyDictionary<string, ISceneInfo> Scenes { get; }

        void AddLoader(string id, ISceneLoader loader);
        bool RemoveLoader(string id);
        void AddScene(string id, ISceneInfo scene);
        bool RemoveScene(string id);
        void ClearLoaders();
        void ClearScenes();
        ISceneLoader GetLoader(string id);
        bool TryGetLoader(string id, out ISceneLoader loader);
        ISceneInfo GetScene(string id);
        bool TryGetScene(string id, out ISceneInfo scene);
    }
}
