using System.Collections.Generic;
using UnityEngine;

namespace DataDriven
{
    /// <summary>アイテムリストのランタイムデータ</summary>
    public class ItemListRuntimeData : ItemCollectionRuntimeBase<KeyItemState>
    {
        public ItemListRuntimeData(ItemCollectionDefaultData itemCollection)
        {
            _itemCollection = itemCollection;
            //アイテムコレクション生成
            var itemList = _itemCollection.ItemList.Items;
            _arrayLength = itemList.Length;
            _itemDict = new Dictionary<int, KeyItemState>();
            foreach (var item in itemList)
            {
                if (!item) continue;
                _itemDict[(int)item.CollectionNumber] = new KeyItemState(item, false);
            }
            _currentRowIndex = _itemCollection.DefaultRow;
            _currentColumnIndex = _itemCollection.DefaultColumn;
            _columnCount = _itemCollection.ColumnCount;
            //何行あるかを切り上げの処理を施して計算
            _rowCount = itemList.Length / _columnCount + (itemList.Length % _columnCount != 0 ? 1 : 0);
            _currentIndex = _currentRowIndex * _rowCount + _currentColumnIndex;
        }

        /// <summary>
        /// キーアイテムを渡す関数
        /// </summary>
        /// <param name="keyItem">渡すキーアイテム</param>
        /// <returns>アイテムを持っているかどうか</returns>
        public bool GiveItem(KeyItemDefaultData keyItem)
        {
            var num = keyItem.CollectionNumber;
            if (!_itemDict.ContainsKey((int)num)) return false;
            if (!_itemDict[(int)num].IsObtained) return false;
            _itemDict[(int)num].ObtainItem();
            return true;
        }
    }

    /// <summary>キーアイテムの所持情報に関するクラス</summary>
    [System.Serializable]
    public class KeyItemState : IItemCollection
    {
        KeyItemDefaultData _keyItem;
        bool _isObtained;

        public KeyItemDefaultData ItemInfo => _keyItem;
        public bool IsObtained => _isObtained;

        public KeyItemState(KeyItemDefaultData keyItem, bool haveIngame)
        {
            _keyItem = keyItem;
            _isObtained = haveIngame;
        }

        /// <summary>
        /// このアイテムを取得した時に呼び出す関数
        /// </summary>
        public void ObtainItem()
        {
            _isObtained = true;
        }

        /// <summary>
        /// このアイテムを与えた時に呼び出す関数
        /// </summary>
        public void GiveItem()
        {
            _isObtained = false;
        }
    }
}
