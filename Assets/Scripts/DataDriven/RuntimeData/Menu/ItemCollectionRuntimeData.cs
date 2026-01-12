using UnityEngine;

namespace DataDriven
{
    /// <summary>アイテムコレクションのランタイムデータ</summary>
    public class ItemCollectionRuntimeData : IHorizontalArrowInput, IVerticalArrowInput, IRuntime
    {
        ItemCollectionDefaultData _itemCollection;
        ItemObtainInfo[] _itemObtainInfo;
        int _currentIndex;
        int _currentRowIndex;
        int _currentColumnIndex;
        int _rowCount;
        int _columnCount;

        public int CurrentIndex => _currentIndex;

        public ItemCollectionRuntimeData(ItemCollectionDefaultData itemCollection)
        {
            _itemCollection = itemCollection;
            //アイテムコレクション生成
            var itemList = _itemCollection.ItemList.Items;
            _itemObtainInfo = new ItemObtainInfo[itemList.Length];
            for (int i = 0; i < _itemObtainInfo.Length; i++)
            {
                _itemObtainInfo[i] = new ItemObtainInfo(itemList[i], false);
            }
            _currentRowIndex = _itemCollection.DefaultRow;
            _currentColumnIndex = _itemCollection.DefaultColumn;
            _columnCount = _itemCollection.ColumnCount;
            //何行あるかを切り上げの処理を施して計算
            _rowCount = itemList.Length / _columnCount + (itemList.Length % _columnCount != 0 ? 1 : 0);
            _currentIndex = _currentRowIndex * _rowCount + _currentColumnIndex;
        }

        /// <summary>
        /// 現在選択中のアイテムの詳細を返す関数
        /// </summary>
        /// <returns>現在選択中のアイテムの詳細</returns>
        public KeyItemDefaultData GetItemInfo()
        {
            var index = _currentColumnIndex + _currentRowIndex * _rowCount;
            var item = _itemObtainInfo[index];
            return item != null ? item.Item : null;
        }

        void IHorizontalArrowInput.SelectCategory(IndexMove move)
        {
            //現在選択中のスロットが横方向に行き止まりもしくは配列の端っこを指していたらreturn
            if (move == IndexMove.Next)
            {
                if (_currentColumnIndex % _columnCount == (_columnCount - 1) || _currentIndex >= _itemObtainInfo.Length - 1) return;
            }
            else
            {
                if (_currentColumnIndex % _columnCount == 0 || _currentIndex <= 0) return;
            }
            _currentColumnIndex += (int)move;
            _currentIndex += (int)move;
        }

        void IVerticalArrowInput.SelectCategory(IndexMove move)
        {
            //現在選択中のスロットが縦方向に行き止まりだったらreturn
            if (move == IndexMove.Next)
            {
                if (_currentRowIndex >= _rowCount - 1) return;
            }
            else
            {
                if (_currentRowIndex <= 0) return;
            }
            _currentRowIndex += (int)move;
            //最後の行に移る場合に必要な列のインデックスの調整
            var moveValue = (int)move * _columnCount;
            if (_currentRowIndex == _rowCount - 1)
            {
                //最後の行の列の数
                var lastColumnCount = _itemObtainInfo.Length - _currentRowIndex * _columnCount;
                if (_currentColumnIndex > lastColumnCount - 1)
                {
                    //最後の行の列の数よりいくつ離れているかを計算
                    moveValue = (int)move * (_columnCount - (_currentColumnIndex - (lastColumnCount - 1)));
                    _currentColumnIndex = lastColumnCount - 1;
                }
            }
            _currentIndex += moveValue;
        }
    }

    /// <summary>アイテムのこれまでの獲得情報を保持するクラス</summary>
    [System.Serializable]
    public class ItemObtainInfo
    {
        KeyItemDefaultData _item;
        bool _isObtained;

        public KeyItemDefaultData Item => _item;
        public bool IsObtained => _isObtained;

        public ItemObtainInfo(KeyItemDefaultData item, bool isObtained)
        {
            _item = item;
            _isObtained = isObtained;
        }

        public void ItemObtain(KeyItemDefaultData item)
        {

        }
    }
}
