using DataDriven.Item;
using UnityEngine;

namespace DataDriven
{
    /// <summary>アイテムの初期データのベースクラス</summary>
    public class ItemDefaultData : ScriptableObject
    {
        [Header("ItemInfo")]
        [SerializeField] Sprite _itemImage;
        [SerializeField] string _itemName;
        [SerializeField, TextArea] string _itemInfo;
        [SerializeField] ItemType _itemType;

        public Sprite ItemImage => _itemImage;
        public string ItemName => _itemName;
        public string ItemInfo => _itemInfo;
        public ItemType ItemType => _itemType;
    }

    /// <summary>プレイヤーが使用できるアイテムの初期データ</summary>
    public class UsableItemDefaultData : ItemDefaultData
    {
        [Header("ItemEffect")]
        [SerializeField] float _saturate = 10;

        public float Saturate => _saturate;
    }

    namespace Item
    {
        public enum ItemType
        {
            GoodFood,
            BadFood,
            KeyItem
        }
    }
}
