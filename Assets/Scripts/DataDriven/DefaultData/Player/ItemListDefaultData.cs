using UnityEngine;

namespace DataDriven
{
    /// <summary>アイテムリストの初期データ</summary>
    [CreateAssetMenu(fileName = "ItemList", menuName = "Player/ItemList")]
    public class ItemListDefaultData : ScriptableObject
    {
        /// <summary>アイテムリストが保持するキーアイテムの配列</summary>
        [SerializeField] KeyItemDefaultData[] _items;

        public KeyItemDefaultData[] Items => _items;
    }
}
