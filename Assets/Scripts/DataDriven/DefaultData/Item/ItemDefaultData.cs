using UnityEngine;

namespace DataDriven
{
    /// <summary>アイテムの初期データのベースクラス</summary>
    public class ItemDefaultData : ScriptableObject
    {
        [Header("ItemInfo")]
        [SerializeField] Sprite _itemImage;
        [SerializeField] ItemType _itemType;
        [SerializeField] ItemName _itemName;
        [SerializeField, TextArea] string _itemInfo;

        public Sprite ItemImage => _itemImage;
        public ItemType ItemType => _itemType;
        public ItemName ItemName => _itemName;
        public string ItemInfo => _itemInfo;
    }

    /// <summary>プレイヤーが使用できるアイテムの初期データ</summary>
    public class UsableItemDefaultData : ItemDefaultData
    {
        [Header("ItemEffect")]
        [SerializeField] float _saturate = 10;

        public float Saturate => _saturate;
    }
}
