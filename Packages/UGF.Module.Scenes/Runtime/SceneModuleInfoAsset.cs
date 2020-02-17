using UGF.Application.Runtime;
using UGF.Module.Elements.Runtime;
using UnityEngine;

namespace UGF.Module.Scenes.Runtime
{
    [CreateAssetMenu(menuName = "UGF/Module.Scenes/SceneModuleInfo", order = 2000)]
    public class SceneModuleInfoAsset : ApplicationModuleInfoAsset<ISceneModule>
    {
        protected override IApplicationModule OnBuild(IApplication application)
        {
            var elementModule = application.GetModule<IElementModule>();

            return new SceneModule(elementModule.Context);
        }
    }
}
