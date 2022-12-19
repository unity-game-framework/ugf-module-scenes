using System;
using UGF.EditorTools.Runtime.Ids;
using UnityEngine;

namespace UGF.Module.Scenes.Runtime
{
    [Serializable]
    public struct SceneReference
    {
        [SerializeField] private GlobalId m_guid;
        [SerializeField] private string m_path;

        public GlobalId Guid { get { return m_guid.IsValid() ? m_guid : throw new ArgumentException("Value not specified."); } set { m_guid = value; } }
        public bool HasGuid { get { return m_guid.IsValid(); } }
        public string Path { get { return HasPath ? m_path : throw new ArgumentException("Value not specified."); } set { m_path = value; } }
        public bool HasPath { get { return !string.IsNullOrEmpty(m_path); } }

        public SceneReference(GlobalId guid, string path)
        {
            if (!guid.IsValid()) throw new ArgumentException("Value should be valid.", nameof(guid));
            if (string.IsNullOrEmpty(path)) throw new ArgumentException("Value cannot be null or empty.", nameof(path));

            m_guid = guid;
            m_path = path;
        }

        public bool IsValid()
        {
            return m_guid.IsValid() && !string.IsNullOrEmpty(m_path);
        }

        public bool Equals(SceneReference other)
        {
            return m_guid.Equals(other.m_guid) && m_path == other.m_path;
        }

        public override bool Equals(object obj)
        {
            return obj is SceneReference other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(m_guid, m_path);
        }

        public static bool operator ==(SceneReference first, SceneReference second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(SceneReference first, SceneReference second)
        {
            return !first.Equals(second);
        }

        public override string ToString()
        {
            return $"{m_path} ({m_guid})";
        }
    }
}
