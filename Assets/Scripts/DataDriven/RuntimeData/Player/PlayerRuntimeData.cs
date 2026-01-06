using UnityEngine;

namespace DataDriven
{
    /// <summary>プレイヤーのランタイムデータ</summary>
    public class PlayerRuntimeData : IRuntime
    {
        PlayerDefaultData _player;
        HotbarRuntimeData _hotbar;
        ItemListRuntimeData _itemList;

        float _currentHP;
        float _currentFullness;
        float _speed;
        float _maxWalkSpeed;
        float _maxRunSpeed;
        float _jump;

        public float CurrentHP => _currentHP;
        public float CurrentFullness => _currentFullness;
        public float Speed => _speed;
        public float MaxWalkSpeed => _maxWalkSpeed;
        public float MaxRunSpeed => _maxRunSpeed;
        public float Jump => _jump;

        public HotbarRuntimeData Hotbar => _hotbar;

        public PlayerRuntimeData(PlayerDefaultData player, HotbarRuntimeData hotbar, ItemListRuntimeData itemList)
        {
            _player = player;
            _hotbar = hotbar;
            _itemList = itemList;

            _currentHP = _player.HP;
            _currentFullness = _player.Fullness;
            _speed = _player.Speed;
            _maxWalkSpeed = _player.MaxWalkSpeed;
            _maxRunSpeed = _player.MaxRunSpeed;
            _jump = _player.Jump;
        }

        /// <summary>
        /// アイテムを獲得する関数
        /// </summary>
        /// <param name="item">獲得したアイテム</param>
        /// <returns>アイテムを獲得できたかどうか</returns>
        public bool GetItem(ItemDefaultData item)
        {
            if (item == null) return false;
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
        /// <param name="index">選択するスロット</param>
        public void ItemSelectForKeyboard(int index)
        {
            _hotbar.SelectItemForKeyboard(index);
        }

        /// <summary>
        /// アイテムを選択する関数
        /// </summary>
        /// <param name="dir">選択するスロットをずらす方向</param>
        public void ItemSelectForGamePad(int dir)
        {
            _hotbar.SelectItemForGamePad(dir);
        }

        /// <summary>
        /// アイテムを使用する関数
        /// </summary>
        /// <returns>使用するアイテム</returns>
        public UsableItemDefaultData UseItem()
        {
            return _hotbar.UseItem();
        }

        /// <summary>
        /// 会話中にアイテムを選択する関数
        /// </summary>
        /// <param name="index">選択するスロット</param>
        public void ItemSelectOnConversationForKeyboard(int index)
        {
            _hotbar.SelectItemOnConversationForKeyboard(index);
        }

        /// <summary>
        /// 会話中にアイテムを選択する関数
        /// </summary>
        /// <param name="dir">選択するスロットをずらす方向</param>
        public void ItemSelectOnConversationForGamePad(int dir)
        {
            _hotbar.SelectItemOnConversationForGamePad(dir);
        }

        /// <summary>
        /// アイテムをあげる関数
        /// </summary>
        /// <returns>あげるアイテム</returns>
        public UsableItemDefaultData GiveItem()
        {
            return _hotbar.GiveItem();
        }

        /// <summary>
        /// 特定のアイテムをあげる関数
        /// </summary>
        /// <param name="item">指定のアイテム</param>
        /// <returns>指定のアイテムを持っているかどうか</returns>
        public bool GiveSpecificItem(ItemDefaultData item)
        {
            if (item.ItemType == ItemType.KeyItem)
            {
                return _itemList.GiveItem((KeyItemDefaultData)item);
            }
            else
            {
                return _hotbar.GiveSpecificItem((UsableItemDefaultData)item);
            }
        }

        /// <summary>
        /// 満腹度を回復する関数
        /// </summary>
        /// <param name="fullness">回復量</param>
        public void Saturation(float fullness)
        {
            _currentFullness += fullness;
            if (_currentFullness >= _player.Fullness) _currentFullness = _player.Fullness;
            Debug.Log($"Saturation => {_currentFullness}");
        }

        /// <summary>
        /// 必要に応じてHPを更新する関数
        /// </summary>
        /// <param name="value">変化量</param>
        public void ChangeHP(float value)
        {
            _currentHP += value;
            if (_currentHP >= _player.HP) _currentHP = _player.HP;
            if (_currentHP <= 0) _currentHP = 0;
            Debug.Log($"HP => {_currentHP}");
        }
    }
}
