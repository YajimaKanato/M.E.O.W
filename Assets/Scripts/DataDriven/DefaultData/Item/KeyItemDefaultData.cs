using UnityEngine;

namespace DataDriven
{
    /// <summary>キーアイテムの初期データ</summary>
    [CreateAssetMenu(fileName = "KeyItem", menuName = "Item/KeyItem")]
    public class KeyItemDefaultData : ItemDefaultData
    {
        /// <summary>キーアイテムの識別番号</summary>
        [SerializeField] KeyItemNum _collectionNumber;

        public KeyItemNum CollectionNumber => _collectionNumber;
    }
}
