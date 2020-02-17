using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime
{
    [Serializable]
    public struct SceneLoadParameters
    {
        [SerializeField] private LoadSceneMode m_addMode;
        [SerializeField] private LocalPhysicsMode m_physicsMode;

        public LoadSceneMode AddMode { get { return m_addMode; } set { m_addMode = value; } }
        public LocalPhysicsMode PhysicsMode { get { return m_physicsMode; } set { m_physicsMode = value; } }
    }
}
