using UnityEngine;

namespace DataDriven
{
    /// <summary>アイテムを渡すイベントのクラス</summary>
    [CreateAssetMenu(fileName = "GiveItemEvent", menuName = "Event/EventParts/GiveItemEvent")]
    public class GiveItemEvent : EventParts
    {
        [SerializeField] ItemDefaultData _item;

        public ItemDefaultData Item => _item;
    }
}
