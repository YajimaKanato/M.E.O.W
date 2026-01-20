using UnityEngine;

namespace DataDriven
{
    [CreateAssetMenu(fileName = "SceneTransitionEvent", menuName = "Event/EventParts/SceneTransitionEvent")]
    public class SceneTransitionEvent : EventParts
    {
        [SerializeField] SceneName _sceneName;
        [SerializeField] TransitionPoint _point;

        public SceneName SceneName => _sceneName;
        public TransitionPoint Point => _point;
    }
}
