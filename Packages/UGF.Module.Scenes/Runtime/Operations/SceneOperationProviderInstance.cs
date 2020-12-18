using System;

namespace UGF.Module.Scenes.Runtime.Operations
{
    public static class SceneOperationProviderInstance
    {
        public static ISceneOperationProvider Provider { get { return m_provider; } set { m_provider = value ?? throw new ArgumentNullException(nameof(value)); } }

        private static ISceneOperationProvider m_provider = new SceneOperationProvider();
    }
}
