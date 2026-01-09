using System.Collections.Generic;
using UnityEngine;

namespace DataDriven
{
    /// <summary>アイテムリストのランタイムデータ</summary>
    public class ItemListRuntimeData : IRuntime
    {
        /// <summary>固有番号とアイテムの情報を結びつけて保持する辞書</summary>
        Dictionary<int, KeyItemState> _itemDict;

        public ItemListRuntimeData(ItemListDefaultData itemList)
        {
            //固有番号に対してアイテムの獲得情報を生成する
            _itemDict = new Dictionary<int, KeyItemState>();
            foreach (var item in itemList.Items)
            {
                if (!item) continue;
                var num = item.CollectionNumber;
                _itemDict[num] = new KeyItemState(item, false, false);
            }
        }

        /// <summary>
        /// キーアイテムを獲得する関数
        /// </summary>
        /// <param name="keyItem">獲得したキーアイテム</param>
        public void GetKeyItem(KeyItemDefaultData keyItem)
        {
            var num = keyItem.CollectionNumber;
            if (!_itemDict.ContainsKey(num))
            {
                Debug.Log($"{keyItem.ItemName}'s Number {num} was not found");
                return;
            }
            else
            {
                _itemDict[num].GetItem();
                Debug.Log($"Get => {keyItem.ItemName}");
            }
        }

        /// <summary>
        /// キーアイテムを渡す関数
        /// </summary>
        /// <param name="keyItem">渡すキーアイテム</param>
        /// <returns>アイテムを持っているかどうか</returns>
        public bool GiveItem(KeyItemDefaultData keyItem)
        {
            var num = keyItem.CollectionNumber;
            if (!_itemDict.ContainsKey(num))
            {
                Debug.Log($"{keyItem.ItemName}'s Number {num} was not found");
                return false;
            }
            else
            {
                _itemDict[num].GiveItem();
                Debug.Log($"Give => {keyItem.ItemName}");
                return true;
            }
        }
    }
}
