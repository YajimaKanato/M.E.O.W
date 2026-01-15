using UnityEngine;

namespace DataDriven
{
    /// <summary>アイテムコレクションの初期データ</summary>
    [CreateAssetMenu(fileName = "ItemCollection", menuName = "Menu/MenuCategory/ItemCollection")]
    public class ItemCollectionDefaultData : ScriptableObject
    {
        [SerializeField] ItemList _itemList;
        [SerializeField, Tooltip("列")] int _columnCount = 5;
        [SerializeField] int _defaultRow = 0;
        [SerializeField] int _defaultColumn = 0;

        public ItemList ItemList => _itemList;
        public int ColumnCount => _columnCount;
        public int DefaultRow => _defaultRow;
        public int DefaultColumn => _defaultColumn;
    }
}
