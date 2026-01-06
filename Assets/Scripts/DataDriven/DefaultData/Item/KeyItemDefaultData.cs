using UnityEngine;

namespace DataDriven
{
    /// <summary>キーアイテムの初期データ</summary>
    [CreateAssetMenu(fileName = "KeyItem", menuName = "Item/KeyItem")]
    public class KeyItemDefaultData : ItemDefaultData
    {
        /// <summary>キーアイテムの識別番号</summary>
        [SerializeField, Range(0, 100)] int _collectionNumber;

        public int CollectionNumber => _collectionNumber;
    }
}
