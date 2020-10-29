using System;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime
{
    public class SceneInstance
    {
        public Scene Scene { get; }
        public string Id { get; }
        public SceneRoot Root { get; }

        public SceneInstance(Scene scene, string id)
        {
            if (!scene.IsValid()) throw new ArgumentException($"Specified scene is not valid: '{scene.name}'.");
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));

            Scene = scene;
            Id = id;
            Root = new SceneRoot(scene);
        }

        public SceneContainer GetContainer()
        {
            return TryGetContainer(out SceneContainer container) ? container : throw new ArgumentException($"Container not specified at this scene: '{Scene.name}'.");
        }

        public bool TryGetContainer(out SceneContainer container)
        {
            return Root.TryGetComponent(out container);
        }
    }
}
