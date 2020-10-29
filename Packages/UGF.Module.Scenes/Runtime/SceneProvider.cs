using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UGF.Module.Scenes.Runtime
{
    public class SceneProvider : ISceneProvider
    {
        public IReadOnlyDictionary<string, ISceneLoader> Loaders { get; }
        public IReadOnlyDictionary<string, ISceneInfo> Scenes { get; }

        private readonly Dictionary<string, ISceneLoader> m_loaders = new Dictionary<string, ISceneLoader>();
        private readonly Dictionary<string, ISceneInfo> m_scenes = new Dictionary<string, ISceneInfo>();

        public SceneProvider()
        {
            Loaders = new ReadOnlyDictionary<string, ISceneLoader>(m_loaders);
            Scenes = new ReadOnlyDictionary<string, ISceneInfo>(m_scenes);
        }

        public void AddLoader(string id, ISceneLoader loader)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (loader == null) throw new ArgumentNullException(nameof(loader));

            m_loaders.Add(id, loader);
        }

        public bool RemoveLoader(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));

            return m_loaders.Remove(id);
        }

        public void AddScene(string id, ISceneInfo scene)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (scene == null) throw new ArgumentNullException(nameof(scene));

            m_scenes.Add(id, scene);
        }

        public bool RemoveScene(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));

            return m_scenes.Remove(id);
        }

        public void ClearLoaders()
        {
            m_loaders.Clear();
        }

        public void ClearScenes()
        {
            m_scenes.Clear();
        }

        public ISceneLoader GetLoader(string id)
        {
            return TryGetLoader(id, out ISceneLoader loader) ? loader : throw new ArgumentException($"Scene loader not found by the specified id: '{id}'.");
        }

        public bool TryGetLoader(string id, out ISceneLoader loader)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));

            return m_loaders.TryGetValue(id, out loader);
        }

        public ISceneInfo GetScene(string id)
        {
            return TryGetScene(id, out ISceneInfo scene) ? scene : throw new ArgumentException($"Scene info not found by the specified id: '{id}'.");
        }

        public bool TryGetScene(string id, out ISceneInfo scene)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));

            return m_scenes.TryGetValue(id, out scene);
        }
    }
}
