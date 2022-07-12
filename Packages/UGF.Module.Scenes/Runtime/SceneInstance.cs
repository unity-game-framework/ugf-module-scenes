using System;
using UGF.EditorTools.Runtime.Ids;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime
{
    public class SceneInstance
    {
        public Scene Scene { get; }
        public GlobalId Id { get; }
        public SceneRoot Root { get; }

        public SceneInstance(Scene scene, GlobalId id)
        {
            if (!scene.IsValid()) throw new ArgumentException("Value should be valid.", nameof(scene));
            if (!id.IsValid()) throw new ArgumentException("Value should be valid.", nameof(id));

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
