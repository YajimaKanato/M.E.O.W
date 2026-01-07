using UnityEngine;

namespace DataDriven
{
    /// <summary>アイテムコレクションのランタイムデータ</summary>
    public class ItemCollectionRuntimeData
    {
        ItemCollectionDefaultData _itemCollection;

        public ItemCollectionRuntimeData(ItemCollectionDefaultData itemCollection)
        {
            _itemCollection = itemCollection;
        }
    }
}
