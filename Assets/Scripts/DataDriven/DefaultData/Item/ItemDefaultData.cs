using DataDriven.Item;
using UnityEngine;

namespace DataDriven
{
    /// <summary>アイテムの初期データのベースクラス</summary>
    public class ItemDefaultData : ScriptableObject
    {
        [Header("ItemInfo")]
        [SerializeField] Sprite _itemImage;
        [SerializeField] ItemName _itemName;
        [SerializeField, TextArea] string _itemInfo;
        [SerializeField] ItemType _itemType;

        public Sprite ItemImage => _itemImage;
        public ItemName ItemName => _itemName;
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
        /// <summary>アイテムの種類</summary>
        public enum ItemType
        {
            GoodFood,
            BadFood,
            KeyItem
        }

        /// <summary>アイテムの名前</summary>
        public enum ItemName
        {
            [InspectorName("肉")] Meat,
            [InspectorName("チーズ")] Cheese,
            [InspectorName("魚")] Fish,
            [InspectorName("腐った肉")] RottenMeat,
            [InspectorName("お酒")] Alcohol,
            [InspectorName("チョコ")] Chocolate,
            [InspectorName("犬の首輪")] DogCollar,
            [InspectorName("猫の首輪")] CatCollar,
            [InspectorName("倉庫のキー")] StorageKey,
            [InspectorName("ロープ")] Rope,
            [InspectorName("倉庫の地図")] StorageMap,
            [InspectorName("カメラ")] Camera,
            [InspectorName("メモリーカード")] MemoryCard,
            [InspectorName("おもちゃ")] Toy,
            [InspectorName("装置の解読コード")] DecodingCord
        }
    }
}
