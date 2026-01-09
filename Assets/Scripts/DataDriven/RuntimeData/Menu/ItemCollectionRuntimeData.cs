using UnityEngine;

namespace DataDriven
{
    /// <summary>アイテムコレクションのランタイムデータ</summary>
    public class ItemCollectionRuntimeData : MenuCategoryRuntime, IRuntime
    {
        ItemCollectionDefaultData _itemCollection;

        public ItemCollectionRuntimeData(ItemCollectionDefaultData itemCollection)
        {
            _itemCollection = itemCollection;
        }

        public override void SelectCategory(SlotMoveVertical move)
        {

        }
    }
}
