using UnityEngine;

namespace UGF.Module.Scenes.Runtime.Tests
{
    public class TestSceneContainer : SceneContainer
    {
        [SerializeField] private bool m_boolValue;
        [SerializeField] private int m_intValue;
        [SerializeField] private float m_floatValue;

        public bool BoolValue { get { return m_boolValue; } set { m_boolValue = value; } }
        public int IntValue { get { return m_intValue; } set { m_intValue = value; } }
        public float FloatValue { get { return m_floatValue; } set { m_floatValue = value; } }
    }
}
