using System;

namespace UGF.Module.Scenes.Runtime.Loaders.Manager
{
    public class ManagerSceneInfo : ISceneInfo
    {
        public string LoaderId { get; }
        public string SceneId { get; }

        string ISceneInfo.Address { get { return SceneId; } }

        public ManagerSceneInfo(string loaderId, string sceneId)
        {
            if (string.IsNullOrEmpty(loaderId)) throw new ArgumentException("Value cannot be null or empty.", nameof(loaderId));
            if (string.IsNullOrEmpty(sceneId)) throw new ArgumentException("Value cannot be null or empty.", nameof(sceneId));

            LoaderId = loaderId;
            SceneId = sceneId;
        }
    }
}
