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
        T GetScene<T>(string id) where T : class, ISceneInfo;
        ISceneInfo GetScene(string id);
        bool TryGetScene<T>(string id, out T scene) where T : class, ISceneInfo;
        bool TryGetScene(string id, out ISceneInfo scene);
    }
}
