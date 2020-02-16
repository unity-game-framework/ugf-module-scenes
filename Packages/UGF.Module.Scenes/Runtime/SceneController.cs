using System;
using System.Collections.Generic;
using UGF.Initialize.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime
{
    public class SceneController : InitializeBase
    {
        public Scene Scene { get; }
        public ISceneDescription Description { get; }
        public SceneRoot Root { get; }
        public SceneContainer Container { get { return m_container ? m_container : throw new ArgumentException("Container not specified."); } }
        public bool HasContainer { get { return m_container != null; } }

        private readonly SceneContainer m_container;
        private readonly List<GameObject> m_rootBuffer = new List<GameObject>();

        public SceneController(Scene scene, ISceneDescription description)
        {
            if (!scene.IsValid()) throw new ArgumentException("Scene not valid.");

            Scene = scene;
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Root = new SceneRoot(scene);

            Root.TryGetComponent(out m_container);
        }
    }
}
