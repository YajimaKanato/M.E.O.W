using UnityEngine;

namespace DataDriven
{
    [CreateAssetMenu(fileName = "SceneTransitionEvent", menuName = "Event/EventParts/SceneTransitionEvent")]
    public class SceneTransitionEvent : EventParts
    {
        [SerializeField] SceneName _sceneName;

        public SceneName SceneName => _sceneName;
    }
}
