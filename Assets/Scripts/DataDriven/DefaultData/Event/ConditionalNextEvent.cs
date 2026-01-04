using UnityEngine;

namespace DataDriven
{
    /// <summary>条件付きで次に進めるイベントのクラス</summary>
    [CreateAssetMenu(fileName = "ConditionalNextEvent", menuName = "Event/EventParts/ConditionalNextEvent")]
    public class ConditionalNextEvent : EventParts
    {
        [SerializeField] ItemDefaultData _keyItem;

        public ItemDefaultData KeyItem => _keyItem;
    }
}
