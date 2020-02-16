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
        public SceneContainer Container { get { return m_container ? m_container : throw new ArgumentException("Container not specified."); } }
        public bool HasContainer { get { return m_container != null; } }

        private readonly SceneContainer m_container;
        private readonly List<GameObject> m_rootBuffer = new List<GameObject>();

        public SceneController(Scene scene, ISceneDescription description)
        {
            if (!scene.IsValid()) throw new ArgumentException("Scene not valid.");

            Scene = scene;
            Description = description ?? throw new ArgumentNullException(nameof(description));

            TryGetRootComponent(out m_container);
        }

        public GameObject GetRootGameObject(string name)
        {
            if (!TryGetRootGameObject(name, out GameObject gameObject))
            {
                throw new ArgumentException($"Root gameobject by the specified name not found: '{name}'.");
            }

            return gameObject;
        }

        public bool TryGetRootGameObject(string name, out GameObject gameObject)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            Scene.GetRootGameObjects(m_rootBuffer);

            for (int i = 0; i < m_rootBuffer.Count; i++)
            {
                gameObject = m_rootBuffer[i];

                if (gameObject.name == name)
                {
                    m_rootBuffer.Clear();
                    return true;
                }
            }

            m_rootBuffer.Clear();

            gameObject = null;
            return false;
        }

        public GameObject GetRootGameObjectByTag(string tag)
        {
            if (!TryGetRootGameObjectByTag(tag, out GameObject gameObject))
            {
                throw new ArgumentException($"Root gameobject by the specified tag not found: '{tag}'.");
            }

            return gameObject;
        }

        public bool TryGetRootGameObjectByTag(string tag, out GameObject gameObject)
        {
            if (string.IsNullOrEmpty(tag)) throw new ArgumentException("Value cannot be null or empty.", nameof(tag));

            Scene.GetRootGameObjects(m_rootBuffer);

            for (int i = 0; i < m_rootBuffer.Count; i++)
            {
                gameObject = m_rootBuffer[i];

                if (gameObject.CompareTag(tag))
                {
                    m_rootBuffer.Clear();
                    return true;
                }
            }

            m_rootBuffer.Clear();

            gameObject = null;
            return false;
        }

        public T GetRootComponent<T>()
        {
            return (T)(object)GetRootComponent(typeof(T));
        }

        public Component GetRootComponent(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            if (!TryGetRootComponent(type, out Component component))
            {
                throw new ArgumentException($"Root component by the specified type not found: '{type}'.");
            }

            return component;
        }

        public bool TryGetRootComponent<T>(out T component)
        {
            if (TryGetRootComponent(typeof(T), out Component value) && value is T cast)
            {
                component = cast;
                return true;
            }

            component = default;
            return false;
        }

        public bool TryGetRootComponent(Type type, out Component component)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            Scene.GetRootGameObjects(m_rootBuffer);

            for (int i = 0; i < m_rootBuffer.Count; i++)
            {
                GameObject gameObject = m_rootBuffer[i];

                if (gameObject.TryGetComponent(type, out component))
                {
                    m_rootBuffer.Clear();
                    return true;
                }
            }

            m_rootBuffer.Clear();

            component = null;
            return false;
        }
    }
}
