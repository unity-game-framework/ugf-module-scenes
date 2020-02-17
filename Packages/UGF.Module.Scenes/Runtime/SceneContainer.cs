using System;
using System.Collections.Generic;
using UnityEngine;

namespace UGF.Module.Scenes.Runtime
{
    public class SceneContainer : MonoBehaviour
    {
        [SerializeField] private List<MonoBehaviour> m_containers = new List<MonoBehaviour>();

        public List<MonoBehaviour> Containers { get { return m_containers; } }

        public T Get<T>()
        {
            return (T)(object)Get(typeof(T));
        }

        public MonoBehaviour Get(Type type)
        {
            if (!TryGet(type, out MonoBehaviour container))
            {
                throw new ArgumentException($"Container by the specified type not found: '{type}'.");
            }

            return container;
        }

        public bool TryGet<T>(out T container)
        {
            if (TryGet(typeof(T), out MonoBehaviour value))
            {
                container = (T)(object)value;
                return true;
            }

            container = default;
            return false;
        }

        public bool TryGet(Type type, out MonoBehaviour container)
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
