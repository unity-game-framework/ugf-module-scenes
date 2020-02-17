using System;
using UnityEngine;

namespace UGF.Module.Scenes.Runtime
{
    [Serializable]
    public struct SceneUnloadParameters
    {
        [SerializeField] private bool m_unloadAllEmbeddedSceneObjects;

        public bool UnloadAllEmbeddedSceneObjects { get { return m_unloadAllEmbeddedSceneObjects; } set { m_unloadAllEmbeddedSceneObjects = value; } }
    }
}
