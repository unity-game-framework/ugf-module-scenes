using System;
using System.Collections.Generic;
using UGF.EditorTools.Runtime.Ids;
using UnityEngine;

namespace UGF.Module.Scenes.Runtime
{
    public abstract class SceneGroupAsset : ScriptableObject
    {
        public void GetScenes(IDictionary<GlobalId, ISceneInfo> scenes)
        {
            if (scenes == null) throw new ArgumentNullException(nameof(scenes));

            OnGetScenes(scenes);
        }

        protected abstract void OnGetScenes(IDictionary<GlobalId, ISceneInfo> scenes);
    }
}
