using UnityEngine;

namespace DataDriven
{
    public class SceneTransitionRuntimeData : IRuntime
    {
        SceneName _sceneName;
        TransitionPoint _transitionPoint;

        public SceneName SceneName => _sceneName;
        public TransitionPoint TransitionPoint => _transitionPoint;

        public SceneTransitionRuntimeData(SceneTransitionDefaultData defaultData)
        {
            _sceneName = defaultData.SceneName;
            _transitionPoint = defaultData.Point;
        }
    }
}
