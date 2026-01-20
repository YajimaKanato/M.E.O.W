using UnityEngine;

namespace DataDriven
{
    [CreateAssetMenu(fileName = "SceneTransitionDefaultData", menuName = "InteractObject/SceneTransitionDefaultData")]
    public class SceneTransitionDefaultData : ScriptableObject
    {
        [SerializeField] SceneName _sceneName;
        [SerializeField] TransitionPoint _point;

        public SceneName SceneName => _sceneName;
        public TransitionPoint Point => _point;
    }
}
