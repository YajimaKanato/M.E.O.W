using UnityEngine;

namespace DataDriven
{
    /// <summary>アイテムの初期データのベースクラス</summary>
    public class ItemDefaultData : ScriptableObject
    {
        [Header("ItemInfo")]
        [SerializeField] Sprite _itemImage;
        [SerializeField] ItemRole _itemType;
        [SerializeField] ItemType _itemName;
        [SerializeField] string _name;
        [SerializeField, TextArea] string _itemInfo;

        public Sprite ItemImage => _itemImage;
        public ItemRole ItemType => _itemType;
        public ItemType ItemName => _itemName;
        public string Name => _name;
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
