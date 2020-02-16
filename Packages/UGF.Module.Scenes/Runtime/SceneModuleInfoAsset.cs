using UGF.Application.Runtime;
using UGF.Elements.Runtime;
using UnityEngine;

namespace UGF.Module.Scenes.Runtime
{
    [CreateAssetMenu(menuName = "UGF/Module.Scenes/SceneModuleInfo", order = 2000)]
    public class SceneModuleInfoAsset : ApplicationModuleInfoAsset<ISceneModule>
    {
        protected override IApplicationModule OnBuild(IApplication application)
        {
            var context = new ElementContext { application };

            return new SceneModule(context);
        }
    }
}
