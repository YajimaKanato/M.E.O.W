using UnityEngine;

namespace DataDriven
{
    /// <summary>ホットバーのランタイムデータ</summary>
    public class HotbarRuntimeData : IRuntime
    {
        UsableItemDefaultData[] _hotbar;
        int _currentIndex = 0;
        int _selectItemIndex = 0;

        public UsableItemDefaultData[] Hotbar => _hotbar;

        public HotbarRuntimeData(HotbarDefaultData hotbar)
        {
            _hotbar = new UsableItemDefaultData[hotbar.Hotbar.Length];
            for (int i = 0; i < _hotbar.Length; i++)
            {
                _hotbar[i] = hotbar.Hotbar[i];
            }
        }

        /// <summary>
        /// アイテムをホットバーに追加する関数
        /// </summary>
        /// <param name="item">獲得したアイテム</param>
        /// <returns>ホットバーに追加できたかどうか</returns>
        public bool GetItem(UsableItemDefaultData item)
        {
            for (int i = 0; i < _hotbar.Length; i++)
            {
                // 空いているスロットを探す
                if (_hotbar[i] == null)
                {
                    _hotbar[i] = item;
                    Debug.Log($"Get => {item.ItemName}");
                    return true;
                }
            }

            //空のスロットがなかった時
            Debug.Log($"Couldn't Get Item");
            return false;
        }

        /// <summary>
        /// アイテム交換をする関数
        /// </summary>
        /// <param name="item">獲得したアイテム</param>
        /// <param name="index">交換するアイテムが格納されたインデックス</param>
        /// <returns>交換したアイテム</returns>
        public ItemDefaultData ChangeItem(UsableItemDefaultData item, int index)
        {
            var changeItem = _hotbar[index];
            _hotbar[index] = item;
            return changeItem;
        }

        /// <summary>
        /// アイテムを選択する関数
        /// </summary>
        /// <param name="index">選択するスロット</param>
        public void SelectItemForKeyboard(int index)
        {
            if (index < 0 || _hotbar.Length - 1 < index) return;
            _currentIndex = index;
            Debug.Log($"Select => {_currentIndex} : {_hotbar[_currentIndex]}");
        }

        /// <summary>
        /// アイテムを選択する関数
        /// </summary>
        /// <param name="dir">選択するスロットをずらす方向</param>
        public void SelectItemForGamePad(IndexMove dir)
        {
            _currentIndex += (int)dir;
            if (_currentIndex > _hotbar.Length - 1)
            {
                _currentIndex = 0;
            }
            else if (_currentIndex < 0)
            {
                _currentIndex = _hotbar.Length - 1;
            }
            Debug.Log($"Select => {_currentIndex} : {_hotbar[_currentIndex]}");
        }

        /// <summary>
        /// アイテムを使用する関数
        /// </summary>
        /// <returns>使用するアイテム</returns>
        public UsableItemDefaultData UseItem()
        {
            var item = _hotbar[_currentIndex];
            _hotbar[_currentIndex] = null;
            return item;
        }

        /// <summary>
        /// アイテムを選択する関数
        /// </summary>
        /// <param name="index">選択するスロット</param>
        public void SelectItemOnConversationForKeyboard(int index)
        {
            if (index < 0 || _hotbar.Length - 1 < index) return;
            _selectItemIndex = index;
            Debug.Log($"Select => {_selectItemIndex} : {_hotbar[_selectItemIndex]}");
        }

        /// <summary>
        /// アイテムを選択する関数
        /// </summary>
        /// <param name="dir">選択するスロットをずらす方向</param>
        public void SelectItemOnConversationForGamePad(IndexMove dir)
        {
            _selectItemIndex += (int)dir;
            if (_selectItemIndex > _hotbar.Length - 1)
            {
                _selectItemIndex = 0;
            }
            else if (_selectItemIndex < 0)
            {
                _selectItemIndex = _hotbar.Length - 1;
            }
            Debug.Log($"Select => {_selectItemIndex} : {_hotbar[_selectItemIndex]}");
        }

        /// <summary>
        /// アイテムをあげる関数
        /// </summary>
        /// <returns>あげるアイテム</returns>
        public UsableItemDefaultData GiveItem()
        {
            var item = _hotbar[_selectItemIndex];
            if (!item) return null;
            _hotbar[_selectItemIndex] = null;
            _selectItemIndex = 0;
            return item;
        }

        /// <summary>
        /// 特定のアイテムをあげる関数
        /// </summary>
        /// <param name="item">指定のアイテム</param>
        /// <returns>指定のアイテムがあるかどうか</returns>
        public bool GiveSpecificItem(UsableItemDefaultData item)
        {
            for (int i = 0; i < _hotbar.Length; i++)
            {
                if (_hotbar[i].ItemName == item.ItemName)
                {
                    _hotbar[i] = null;
                    return true;
                }
            }
            return false;
        }
    }
}
