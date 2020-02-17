using UGF.EditorTools.Runtime.IMGUI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UGF.Module.Scenes.Runtime
{
    [CreateAssetMenu(menuName = "UGF/Module.Scenes/SceneDescription", order = 2000)]
    public class SceneDescription : ScriptableObject, ISceneDescription
    {
        [SerializeField] private string m_name;
        [SerializeField, AssetGuid(typeof(Scene))] private string m_scene;
        [SerializeField] private SceneLoadParameters m_loadParameters;
        [SerializeField] private SceneUnloadParameters m_unloadParameters;

        public string Name { get { return m_name; } set { m_name = value; } }
        public string AssetName { get { return m_scene; } set { m_scene = value; } }
        public SceneLoadParameters LoadParameters { get { return m_loadParameters; } set { m_loadParameters = value; } }
        public SceneUnloadParameters UnloadParameters { get { return m_unloadParameters; } set { m_unloadParameters = value; } }
    }
}
