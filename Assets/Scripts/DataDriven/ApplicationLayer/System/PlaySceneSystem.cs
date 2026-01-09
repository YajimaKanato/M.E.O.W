using System;
using UnityEngine;

namespace DataDriven
{
    /// <summary>プレイ中の処理を司るクラス</summary>
    public class PlaySceneSystem
    {
        RuntimeDataRepository _repository;

        public PlaySceneSystem(RuntimeDataRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// アイテムを選択する関数
        /// </summary>
        /// <param name="index">選択するスロット</param>
        public void HotbarSelectForKetboard(int index)
        {
            if (_repository.TryGetData<HotbarRuntimeData>(DataID.Hotbar, out var hotbar))
            {
                hotbar.SelectItemForKeyboard(index);
            }
        }

        /// <summary>
        /// アイテムを選択する関数
        /// </summary>
        /// <param name="dir">選択するスロットをずらす方向</param>
        public void HotbarSelectForGamePad(IndexMove dir)
        {
            if (_repository.TryGetData<HotbarRuntimeData>(DataID.Hotbar, out var hotbar))
            {
                hotbar.SelectItemForGamePad(dir);
            }
        }

        /// <summary>
        /// アイテムを使用する関数
        /// </summary>
        public void UseItem()
        {
            if (_repository.TryGetData<HotbarRuntimeData>(DataID.Hotbar, out var hotbar))
            {
                var item = hotbar.UseItem();
                //空のスロットを選択した時
                if (!item) return;
                if (_repository.TryGetData<PlayerRuntimeData>(DataID.Player, out var player))
                {
                    //悪影響を及ぼす食べ物の場合ダメージ
                    if (item.ItemType == ItemType.BadFood) player.ChangeHP(((BadFoodDefaultData)item).Damage * (-1));
                    player.Saturation(item.Saturate);
                    Debug.Log($"HP => {player.CurrentHP}\nSaturation => {player.CurrentFullness}");
                }
            }
        }
    }
}
