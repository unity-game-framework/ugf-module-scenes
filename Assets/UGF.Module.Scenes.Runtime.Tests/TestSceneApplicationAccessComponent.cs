using NUnit.Framework;
using UGF.Application.Runtime;
using UnityEngine;

namespace UGF.Module.Scenes.Runtime.Tests
{
    public class TestSceneApplicationAccessComponent : MonoBehaviour
    {
        [SerializeField] private ApplicationAccessComponent m_application;

        public ApplicationAccessComponent Application { get { return m_application; } set { m_application = value; } }

        private void Awake()
        {
            Check();
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
