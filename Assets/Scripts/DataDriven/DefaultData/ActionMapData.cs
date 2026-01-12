using UnityEngine;

namespace DataDriven
{
    /// <summary>シーンごとのアクションマップのデータ</summary>
    [CreateAssetMenu(fileName = "ActionMapData", menuName = "ActionMap/ActionMapData")]
    public class ActionMapData : ScriptableObject
    {
        [SerializeField] ActionMapWithScene[] _pair;

        public ActionMapWithScene[] Pair => _pair;
    }

    /// <summary>シーンとアクションマップのペア</summary>
    [System.Serializable]
    public class ActionMapWithScene
    {
        [SerializeField] string _sceneName;
        [SerializeField] ActionMapName _actionMapName;

        public string SceneName => _sceneName;
        public ActionMapName ActionMapName => _actionMapName;
    }
}
