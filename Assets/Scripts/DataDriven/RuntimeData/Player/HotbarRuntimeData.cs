using UnityEngine;

namespace DataDriven
{
    /// <summary>ホットバーのランタイムデータ</summary>
    public class HotbarRuntimeData
    {
        UsableItemDefaultData[] _hotbar;
        int _hotbarCount = 6;
        int _currentIndex = 0;
        int _selectItemIndex = 0;

        public UsableItemDefaultData[] Hotbar => _hotbar;

        public HotbarRuntimeData()
        {
            _hotbar = new UsableItemDefaultData[_hotbarCount];
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
        /// <param name="dir">選択するスロットをずらす方向</param>
        public void SelectItem(int dir)
        {
            _currentIndex += dir;
            if (_currentIndex > _hotbar.Length - 1)
            {
                _currentIndex = 0;
            }
            else if (_currentIndex < 0)
            {
                _currentIndex = _hotbar.Length - 1;
            }
        }

        /// <summary>
        /// アイテムを使用する関数
        /// </summary>
        /// <returns>使用するアイテム</returns>
        public UsableItemDefaultData UseItem()
        {
            return _hotbar[_currentIndex];
        }

        /// <summary>
        /// 会話中にアイテムを選択する関数
        /// </summary>
        /// <param name="dir">選択するスロットをずらす方向</param>
        public void ItemSelectOnConversation(int dir)
        {
            _selectItemIndex += dir;
            if (_selectItemIndex > _hotbar.Length - 1)
            {
                _selectItemIndex = 0;
            }
            else if (_selectItemIndex < 0)
            {
                _selectItemIndex = _hotbar.Length - 1;
            }
        }

        /// <summary>
        /// アイテムをあげる関数
        /// </summary>
        /// <returns>あげるアイテム</returns>
        public UsableItemDefaultData GiveItem()
        {
            var item = _hotbar[_selectItemIndex];
            _hotbar[_selectItemIndex] = null;
            _selectItemIndex = 0;
            return item;
        }
    }
}
