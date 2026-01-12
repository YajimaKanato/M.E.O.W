using UnityEngine;

namespace DataDriven
{
    /// <summary>インタラクトできるエンティティをまとめたクラス</summary>
    [CreateAssetMenu(fileName = "EventTargetDefaultData", menuName = "Event/EventTargetDefaultData")]
    public class EventTargetDefaultData : ScriptableObject
    {
        [SerializeField] DataID[] _targets;

        public DataID[] Targets => _targets;
    }
}
