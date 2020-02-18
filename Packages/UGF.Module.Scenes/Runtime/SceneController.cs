using System;
using UGF.Initialize.Runtime;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime
{
    public class SceneController : InitializeBase
    {
        public Scene Scene { get; }
        public SceneRoot Root { get; }
        public SceneContainer Container { get { return m_container ? m_container : throw new ArgumentException("Container not specified."); } }
        public bool HasContainer { get { return m_container != null; } }

        private SceneContainer m_container;

        public SceneController(Scene scene)
        {
            if (!scene.IsValid()) throw new ArgumentException("Scene not valid.");

            Scene = scene;
            Root = new SceneRoot(scene);
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            Root.TryGetComponent(out m_container);
        }

        protected override void OnUninitialize()
        {
            base.OnUninitialize();

            m_container = null;
        }
    }
}
