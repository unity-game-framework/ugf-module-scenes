using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime
{
    [Serializable]
    public class SceneUnloadParameters : ISceneUnloadParameters
    {
        [SerializeField] private UnloadSceneOptions m_options;

        public UnloadSceneOptions Options { get { return m_options; } set { m_options = value; } }

        public static SceneUnloadParameters Default { get; } = new SceneUnloadParameters(UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);

        public SceneUnloadParameters(UnloadSceneOptions options)
        {
            m_options = options;
        }
    }
}
