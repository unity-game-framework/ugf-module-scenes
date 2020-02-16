using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime
{
    public class SceneRoot
    {
        public Scene Scene { get; }

        private readonly List<GameObject> m_buffer = new List<GameObject>();

        public SceneRoot(Scene scene)
        {
            Scene = scene;
        }

        public GameObject GetGameObject(string name)
        {
            if (!TryGetGameObject(name, out GameObject gameObject))
            {
                throw new ArgumentException($"Root gameobject by the specified name not found: '{name}'.");
            }

            return gameObject;
        }

        public bool TryGetGameObject(string name, out GameObject gameObject)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            Scene.GetRootGameObjects(m_buffer);

            for (int i = 0; i < m_buffer.Count; i++)
            {
                gameObject = m_buffer[i];

                if (gameObject.name == name)
                {
                    m_buffer.Clear();
                    return true;
                }
            }

            m_buffer.Clear();

            gameObject = null;
            return false;
        }

        public GameObject GetGameObjectByTag(string tag)
        {
            if (!TryGetGameObjectByTag(tag, out GameObject gameObject))
            {
                throw new ArgumentException($"Root gameobject by the specified tag not found: '{tag}'.");
            }

            return gameObject;
        }

        public bool TryGetGameObjectByTag(string tag, out GameObject gameObject)
        {
            if (string.IsNullOrEmpty(tag)) throw new ArgumentException("Value cannot be null or empty.", nameof(tag));

            Scene.GetRootGameObjects(m_buffer);

            for (int i = 0; i < m_buffer.Count; i++)
            {
                gameObject = m_buffer[i];

                if (gameObject.CompareTag(tag))
                {
                    m_buffer.Clear();
                    return true;
                }
            }

            m_buffer.Clear();

            gameObject = null;
            return false;
        }

        public T GetComponent<T>()
        {
            return (T)(object)GetComponent(typeof(T));
        }

        public Component GetComponent(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            if (!TryGetComponent(type, out Component component))
            {
                throw new ArgumentException($"Root component by the specified type not found: '{type}'.");
            }

            return component;
        }

        public bool TryGetComponent<T>(out T component)
        {
            if (TryGetComponent(typeof(T), out Component value) && value is T cast)
            {
                component = cast;
                return true;
            }

            component = default;
            return false;
        }

        public bool TryGetComponent(Type type, out Component component)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            Scene.GetRootGameObjects(m_buffer);

            for (int i = 0; i < m_buffer.Count; i++)
            {
                GameObject gameObject = m_buffer[i];

                if (gameObject.TryGetComponent(type, out component))
                {
                    m_buffer.Clear();
                    return true;
                }
            }

            m_buffer.Clear();

            component = null;
            return false;
        }
    }
}
