using System.Collections.Generic;
using UnityEngine;

namespace DataDriven
{
    /// <summary>アイテムコレクションのベースクラス</summary>
    public class ItemCollectionRuntimeBase<T> : IHorizontalArrowInput, IVerticalArrowInput, IRuntime where T : IItemCollection
    {
        protected Dictionary<int, T> _itemDict;
        protected int _currentIndex;
        protected int _currentRowIndex;
        protected int _currentColumnIndex;
        protected int _rowCount;
        protected int _columnCount;

        void IHorizontalArrowInput.SelectCategory(IndexMove move)
        {
            //現在選択中のスロットが横方向に行き止まりもしくは配列の端っこを指していたらreturn
            if (move == IndexMove.Next)
            {
                if (_currentColumnIndex % _columnCount == (_columnCount - 1) || _currentIndex >= _itemDict.Count - 1) return;
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
                var lastColumnCount = _itemDict.Count - _currentRowIndex * _columnCount;
                if (_currentColumnIndex > lastColumnCount - 1)
                {
                    //最後の行の列の数よりいくつ離れているかを計算
                    moveValue = (int)move * (_columnCount - (_currentColumnIndex - (lastColumnCount - 1)));
                    _currentColumnIndex = lastColumnCount - 1;
                }
            }
            _currentIndex += moveValue;
        }

        /// <summary>
        /// キーアイテムを獲得する関数
        /// </summary>
        /// <param name="keyItem">獲得したキーアイテム</param>
        /// <returns>アイテムを獲得できたかどうか</returns>
        public bool GetKeyItem(KeyItemDefaultData keyItem)
        {
            var num = keyItem.CollectionNumber;
            if (!_itemDict.ContainsKey((int)num)) return false;
            if (_itemDict[(int)num].IsObtained) return true;
            _itemDict[(int)num].ObtainItem();
            return true;
        }
    }
}
