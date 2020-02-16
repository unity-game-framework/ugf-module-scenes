using System;
using UGF.Elements.Runtime;
using UGF.Initialize.Runtime;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime
{
    public class SceneController : InitializeBase
    {
        public Scene Scene { get; }
        public ISceneDescription Description { get; }
        public SceneRoot Root { get; }
        public IElementCollection Elements { get { return m_parent.Children; } }
        public SceneContainer Container { get { return m_container ? m_container : throw new ArgumentException("Container not specified."); } }
        public bool HasContainer { get { return m_container != null; } }

        private readonly SceneContainer m_container;
        private readonly ElementParent<IElement> m_parent = new ElementParent<IElement>();

        public SceneController(Scene scene, ISceneDescription description, IElementContext context)
        {
            if (!scene.IsValid()) throw new ArgumentException("Scene not valid.");

            Scene = scene;
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Root = new SceneRoot(scene);

            foreach (IElementBuilder builder in Description.Elements)
            {
                IElement element = builder.Build(context);

                m_parent.Children.Add(element);
            }

            if (Root.TryGetComponent(out m_container))
            {
                for (int i = 0; i < m_container.Containers.Count; i++)
                {
                    if (m_container.Containers[i] is IElementBuilder builder)
                    {
                        IElement element = builder.Build(context);

                        m_parent.Children.Add(element);
                    }
                }
            }
        }

        protected override void OnPostInitialize()
        {
            base.OnPostInitialize();

            m_parent.Initialize();
        }

        protected override void OnPreUninitialize()
        {
            base.OnPreUninitialize();

            m_parent.Uninitialize();
        }
    }
}
