using System;
using UGF.Initialize.Runtime;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime
{
    public class SceneController : InitializeBase
    {
        public Scene Scene { get; }
        public SceneRoot Root { get; }
        public IInitializeCollection Children { get { return m_children; } }
        public SceneContainer Container { get { return m_container ? m_container : throw new ArgumentException("Scene container not specified."); } }
        public bool HasContainer { get { return m_container != null; } }

        private readonly InitializeCollection<IInitialize> m_children = new InitializeCollection<IInitialize>();
        private SceneContainer m_container;

        public SceneController(Scene scene)
        {
            if (!scene.IsValid()) throw new ArgumentException($"Specified scene is not valid: '{scene.name}'.");

            Scene = scene;
            Root = new SceneRoot(scene);
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            Root.TryGetComponent(out m_container);

            m_children.Initialize();
        }

        protected override void OnUninitialize()
        {
            base.OnUninitialize();

            m_children.Uninitialize();
            m_container = null;
        }
    }
}
