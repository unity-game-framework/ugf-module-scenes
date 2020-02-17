using System;
using UGF.Elements.Runtime;
using UGF.Initialize.Runtime;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime
{
    public class SceneController : InitializeBase
    {
        public Scene Scene { get; }
        public IElementContext Context { get; }
        public SceneRoot Root { get; }
        public IElementCollection Elements { get { return m_parent.Children; } }
        public SceneContainer Container { get { return m_container ? m_container : throw new ArgumentException("Container not specified."); } }
        public bool HasContainer { get { return m_container != null; } }

        private readonly ElementParent<IElement> m_parent = new ElementParent<IElement>();
        private SceneContainer m_container;

        public SceneController(Scene scene, IElementContext context)
        {
            if (!scene.IsValid()) throw new ArgumentException("Scene not valid.");

            Scene = scene;
            Context = context ?? throw new ArgumentNullException(nameof(context));
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

        protected override void OnPostInitialize()
        {
            base.OnPostInitialize();

            if (m_container != null)
            {
                for (int i = 0; i < m_container.Containers.Count; i++)
                {
                    if (m_container.Containers[i] is IElementBuilder builder)
                    {
                        IElement element = builder.Build(Context);

                        m_parent.Children.Add(element);
                    }
                }
            }

            m_parent.Initialize();
        }

        protected override void OnPreUninitialize()
        {
            base.OnPreUninitialize();

            m_parent.Uninitialize();
            m_parent.Children.Clear();
        }
    }
}
