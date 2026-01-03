using UnityEngine;
using DataDriven.Item;

namespace DataDriven
{
    /// <summary>プレイヤーのランタイムデータ</summary>
    public class PlayerRuntimeData : IRuntime
    {
        PlayerDefaultData _player;
        HotbarRuntimeData _hotbar;
        ItemListRuntimeData _itemList;

        public PlayerRuntimeData(PlayerDefaultData player, HotbarRuntimeData hotbar, ItemListRuntimeData itemList)
        {
            _player = player;
            _hotbar = hotbar;
            _itemList = itemList;
        }

        /// <summary>
        /// アイテムを獲得する関数
        /// </summary>
        /// <param name="item">獲得したアイテム</param>
        /// <returns>アイテムを獲得できたかどうか</returns>
        public bool GetItem(ItemDefaultData item)
        {
            var returnItem = item;
            if (item.ItemType == ItemType.KeyItem)
            {
                _itemList.GetKeyItem((KeyItemDefaultData)item);
                return true;
            }
            else
            {
                return _hotbar.GetItem((UsableItemDefaultData)item);
            }
        }

        /// <summary>
        /// アイテムを交換する関数
        /// </summary>
        /// <param name="item">獲得したアイテム</param>
        /// <param name="index">交換するアイテムが格納されたインデックス</param>
        public void ChangeItem(ItemDefaultData item, int index)
        {
            _hotbar.ChangeItem((UsableItemDefaultData)item, index);
        }

        /// <summary>
        /// アイテムを選択する関数
        /// </summary>
        /// <param name="dir">選択するスロットをずらす方向</param>
        public void ItemSelect(int dir)
        {
            _hotbar.SelectItem(dir);
        }

        /// <summary>
        /// アイテムを使用する関数
        /// </summary>
        public void UseItem()
        {
            _hotbar.UseItem();
        }
    }
}
