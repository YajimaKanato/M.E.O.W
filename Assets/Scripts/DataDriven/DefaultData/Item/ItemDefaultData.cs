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

    /// <summary>プレイヤーにいい効果を与えるアイテムの初期データ</summary>
    [CreateAssetMenu(fileName = "GoodFood", menuName = "Item/GoodFood")]
    public class GoodFoodDefaultData : UsableItemDefaultData
    {

    }

    /// <summary>プレイヤーに悪い効果も与えるアイテムの初期データ</summary>
    [CreateAssetMenu(fileName = "BadFood", menuName = "Item/BadFood")]
    public class BadFoodDefaultData : UsableItemDefaultData
    {
        [SerializeField] float _damage = 10;

        public float Damage => _damage;
    }

    /// <summary>キーアイテムの初期データ</summary>
    [CreateAssetMenu(fileName = "KeyItem", menuName = "Item/KeyItem")]
    public class KeyItemDefaultData : ItemDefaultData
    {
        /// <summary>キーアイテムの識別番号</summary>
        [SerializeField, Range(0, 100)] int _uniqueNumber;

        public int UniqueNumber => _uniqueNumber;
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
