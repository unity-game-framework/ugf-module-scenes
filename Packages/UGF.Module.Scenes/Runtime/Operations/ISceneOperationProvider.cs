using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime.Operations
{
    public interface ISceneOperationProvider
    {
        IReadOnlyDictionary<Scene, AsyncOperation> Operations { get; }

        event SceneOperationHandler Added;
        event SceneOperationHandler Removed;
        event Action Cleared;

        void Add(Scene scene, AsyncOperation operation);
        bool Remove(Scene scene);
        void Clear();
        AsyncOperation Get(Scene scene);
        bool TryGet(Scene scene, out AsyncOperation operation);
    }
}
