using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime.Operations
{
    public class SceneOperationProvider : ISceneOperationProvider
    {
        public IReadOnlyDictionary<Scene, AsyncOperation> Operations { get; }

        public event SceneOperationHandler Added;
        public event SceneOperationHandler Removed;
        public event Action Cleared;

        private readonly Dictionary<Scene, AsyncOperation> m_operations = new Dictionary<Scene, AsyncOperation>();

        public SceneOperationProvider()
        {
            Operations = new ReadOnlyDictionary<Scene, AsyncOperation>(m_operations);
        }

        public void Add(Scene scene, AsyncOperation operation)
        {
            if (!scene.IsValid()) throw new ArgumentException("Value should be valid.", nameof(scene));
            if (operation == null) throw new ArgumentNullException(nameof(operation));

            m_operations.Add(scene, operation);

            Added?.Invoke(scene, operation);
        }

        public bool Remove(Scene scene)
        {
            if (!scene.IsValid()) throw new ArgumentException("Value should be valid.", nameof(scene));

            if (TryGet(scene, out AsyncOperation operation))
            {
                m_operations.Remove(scene);

                Removed?.Invoke(scene, operation);
                return true;
            }

            return false;
        }

        public void Clear()
        {
            m_operations.Clear();

            Cleared?.Invoke();
        }

        public AsyncOperation Get(Scene scene)
        {
            return TryGet(scene, out AsyncOperation operation) ? operation : throw new ArgumentException($"Scene operation not found by the specified scene: '{scene.name}'.");
        }

        public bool TryGet(Scene scene, out AsyncOperation operation)
        {
            if (!scene.IsValid()) throw new ArgumentException("Value should be valid.", nameof(scene));

            return m_operations.TryGetValue(scene, out operation);
        }
    }
}
