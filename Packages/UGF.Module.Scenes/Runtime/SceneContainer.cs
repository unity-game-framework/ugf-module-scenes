using System;
using System.Collections.Generic;
using UnityEngine;

namespace UGF.Module.Scenes.Runtime
{
    [AddComponentMenu("Unity Game Framework/Scenes/Scene Container", 2000)]
    public class SceneContainer : MonoBehaviour
    {
        [SerializeField] private List<Component> m_containers = new List<Component>();

        public List<Component> Containers { get { return m_containers; } }

        public T Get<T>() where T : class
        {
            return (T)(object)Get(typeof(T));
        }

        public Component Get(Type type)
        {
            return TryGet(type, out Component container) ? container : throw new ArgumentException($"Container not found by the specified type: '{type}'.");
        }

        public bool TryGet<T>(out T container) where T : class
        {
            if (TryGet(typeof(T), out Component value))
            {
                container = (T)(object)value;
                return true;
            }

            container = default;
            return false;
        }

        public bool TryGet(Type type, out Component container)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            for (int i = 0; i < m_containers.Count; i++)
            {
                container = m_containers[i];

                if (type.IsInstanceOfType(container))
                {
                    return true;
                }
            }

            container = null;
            return false;
        }
    }
}
