using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime
{
    public class SceneRoot
    {
        public Scene Scene { get; }

        private readonly List<GameObject> m_roots = new List<GameObject>();
        private readonly IReadOnlyList<GameObject> m_rootsReadOnly;

        public SceneRoot(Scene scene)
        {
            if (!scene.IsValid()) throw new ArgumentException($"Specified scene is not valid: '{scene.name}'.");

            Scene = scene;

            m_rootsReadOnly = new ReadOnlyCollection<GameObject>(m_roots);
        }

        public IReadOnlyList<GameObject> GetRootGameObjects()
        {
            Scene.GetRootGameObjects(m_roots);

            return m_rootsReadOnly;
        }

        public GameObject GetGameObject(string name)
        {
            return TryGetGameObject(name, out GameObject gameObject) ? gameObject : throw new ArgumentException($"Root gameobject not found by the specified name: '{name}'.");
        }

        public bool TryGetGameObject(string name, out GameObject gameObject)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Value cannot be null or empty.", nameof(name));

            IReadOnlyList<GameObject> roots = GetRootGameObjects();

            for (int i = 0; i < roots.Count; i++)
            {
                gameObject = roots[i];

                if (gameObject.name == name)
                {
                    return true;
                }
            }

            gameObject = null;
            return false;
        }

        public GameObject GetGameObjectByTag(string tag)
        {
            return TryGetGameObjectByTag(tag, out GameObject gameObject) ? gameObject : throw new ArgumentException($"Root gameobject not found by the specified tag: '{tag}'.");
        }

        public bool TryGetGameObjectByTag(string tag, out GameObject gameObject)
        {
            if (string.IsNullOrEmpty(tag)) throw new ArgumentException("Value cannot be null or empty.", nameof(tag));

            IReadOnlyList<GameObject> roots = GetRootGameObjects();

            for (int i = 0; i < roots.Count; i++)
            {
                gameObject = roots[i];

                if (gameObject.CompareTag(tag))
                {
                    return true;
                }
            }

            gameObject = null;
            return false;
        }

        public T GetComponent<T>() where T : class
        {
            return (T)(object)GetComponent(typeof(T));
        }

        public Component GetComponent(Type type)
        {
            return TryGetComponent(type, out Component component) ? component : throw new ArgumentException($"Component not found in root gameobjects by the specified type: '{type}'.");
        }

        public bool TryGetComponent<T>(out T component) where T : class
        {
            if (TryGetComponent(typeof(T), out Component value))
            {
                component = (T)(object)value;
                return true;
            }

            component = default;
            return false;
        }

        public bool TryGetComponent(Type type, out Component component)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            IReadOnlyList<GameObject> roots = GetRootGameObjects();

            for (int i = 0; i < roots.Count; i++)
            {
                GameObject gameObject = roots[i];

                if (gameObject.TryGetComponent(type, out component))
                {
                    return true;
                }
            }

            component = null;
            return false;
        }
    }
}
