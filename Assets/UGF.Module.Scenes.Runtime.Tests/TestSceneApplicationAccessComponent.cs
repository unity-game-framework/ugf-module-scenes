using NUnit.Framework;
using UGF.Application.Runtime;
using UnityEngine;

namespace UGF.Module.Scenes.Runtime.Tests
{
    public class TestSceneApplicationAccessComponent : MonoBehaviour
    {
        [SerializeField] private ApplicationAccessComponent m_application;
        [SerializeField] private bool m_checkOnAwake;

        public ApplicationAccessComponent Application { get { return m_application; } set { m_application = value; } }
        public bool CheckOnAwake { get { return m_checkOnAwake; } set { m_checkOnAwake = value; } }

        private void Awake()
        {
            if (m_checkOnAwake)
            {
                Check();
            }
        }

        private void Start()
        {
            Check();
        }

        private void OnEnable()
        {
            Check();
        }

        private void Check()
        {
            IApplication application = m_application.GetApplication();

            Assert.NotNull(application);
        }
    }
}
