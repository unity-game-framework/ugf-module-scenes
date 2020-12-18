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
        [SerializeField] private bool m_allowActivation;

        public LoadSceneMode AddMode { get { return m_addMode; } set { m_addMode = value; } }
        public LocalPhysicsMode PhysicsMode { get { return m_physicsMode; } set { m_physicsMode = value; } }
        public bool AllowActivation { get { return m_allowActivation; } set { m_allowActivation = value; } }

        public static SceneLoadParameters Default { get; } = new SceneLoadParameters(LoadSceneMode.Single, LocalPhysicsMode.None);
        public static SceneLoadParameters DefaultAdditive { get; } = new SceneLoadParameters(LoadSceneMode.Additive, LocalPhysicsMode.None);
        public static SceneLoadParameters DefaultAdditiveDisabled { get; } = new SceneLoadParameters(LoadSceneMode.Additive, LocalPhysicsMode.None, false);

        public SceneLoadParameters(LoadSceneMode addMode, LocalPhysicsMode physicsMode, bool allowActivation = true)
        {
            m_addMode = addMode;
            m_physicsMode = physicsMode;
            m_allowActivation = allowActivation;
        }
    }
}
