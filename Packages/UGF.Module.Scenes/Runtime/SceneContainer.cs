using System;
using System.Collections.Generic;
using UnityEngine;

namespace UGF.Module.Scenes.Runtime
{
    public class SceneContainer : MonoBehaviour
    {
        [SerializeField] private List<Component> m_containers = new List<Component>();

        public List<Component> Containers { get { return m_containers; } }

        public T Get<T>()
        {
            return (T)(object)Get(typeof(T));
        }

        public Component Get(Type type)
        {
            if (!TryGet(type, out Component container))
            {
                throw new ArgumentException($"Container by the specified type not found: '{type}'.");
            }

            return container;
        }

        public bool TryGet<T>(out T container)
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
