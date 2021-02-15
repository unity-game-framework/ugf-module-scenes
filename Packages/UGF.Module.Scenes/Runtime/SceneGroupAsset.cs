using System;
using System.Collections.Generic;
using UnityEngine;

namespace UGF.Module.Scenes.Runtime
{
    public abstract class SceneGroupAsset : ScriptableObject
    {
        public void GetScenes(IDictionary<string, ISceneInfo> scenes)
        {
            if (scenes == null) throw new ArgumentNullException(nameof(scenes));

            OnGetScenes(scenes);
        }

        protected abstract void OnGetScenes(IDictionary<string, ISceneInfo> scenes);
    }
}
