using System;
using UGF.EditorTools.Runtime.Ids;
using UGF.RuntimeTools.Runtime.Containers;
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

        public ContainerComponent GetContainer()
        {
            return TryGetContainer(out ContainerComponent container) ? container : throw new ArgumentException($"Container not specified at this scene: '{Scene.name}'.");
        }

        public bool TryGetContainer(out ContainerComponent container)
        {
            return Root.TryGetComponent(out container);
        }
    }
}
