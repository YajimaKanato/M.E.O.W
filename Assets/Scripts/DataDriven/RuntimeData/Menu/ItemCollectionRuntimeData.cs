using System.Collections.Generic;
using UnityEngine;

namespace DataDriven
{
    /// <summary>アイテムコレクションのランタイムデータ</summary>
    public class ItemCollectionRuntimeData : ItemCollectionRuntimeBase<ItemObtainInfo>
    {
        public ItemCollectionRuntimeData(ItemCollectionDefaultData itemCollection)
        {
            _itemCollection = itemCollection;
            //アイテムコレクション生成
            var itemList = _itemCollection.ItemList.Items;
            _arrayLength = itemList.Length;
            _itemDict = new Dictionary<int, ItemObtainInfo>();
            foreach (var item in itemList)
            {
                if (!item) continue;
                _itemDict[(int)item.CollectionNumber] = new ItemObtainInfo(item, false);
            }
            _currentRowIndex = _itemCollection.DefaultRow;
            _currentColumnIndex = _itemCollection.DefaultColumn;
            _columnCount = _itemCollection.ColumnCount;
            //何行あるかを切り上げの処理を施して計算
            _rowCount = itemList.Length / _columnCount + (itemList.Length % _columnCount != 0 ? 1 : 0);
            _currentIndex = _currentRowIndex * _rowCount + _currentColumnIndex;
        }
    }

    /// <summary>アイテムのこれまでの獲得情報を保持するクラス</summary>
    [System.Serializable]
    public class ItemObtainInfo : IItemCollection
    {
        KeyItemDefaultData _item;
        bool _isObtained;

        public KeyItemDefaultData ItemInfo => _item;
        public bool IsObtained => _isObtained;

        public ItemObtainInfo(KeyItemDefaultData item, bool isObtained)
        {
            _item = item;
            _isObtained = isObtained;
        }

        public void ObtainItem()
        {
            _isObtained = true;
        }
    }
}
