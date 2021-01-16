using System;

namespace UGF.Module.Scenes.Runtime
{
    public partial class SceneModuleDescription
    {
        [Obsolete("SceneModuleDescription constructor with 'registerType' argument has been deprecated. Use default constructor and properties initialization instead.")]
        public SceneModuleDescription(Type registerType) : base(registerType)
        {
        }
    }
}
