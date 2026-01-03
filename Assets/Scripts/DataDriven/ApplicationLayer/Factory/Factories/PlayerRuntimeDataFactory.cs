using UnityEngine;

namespace DataDriven
{
    /// <summary>プレイヤーのランタイムデータの作成を司るクラス</summary>
    public class PlayerRuntimeDataFactory
    {
        /// <summary>
        /// プレイヤーのランタイムデータを作成する関数
        /// </summary>
        /// <param name="player">プレイヤーの初期データ</param>
        /// <param name="hotbar">ホットバーのランタイムデータ</param>
        /// <param name="itemList">アイテムリストのランタイムデータ</param>
        /// <returns>プレイヤーのランタイムデータ</returns>
        public PlayerRuntimeData PlayerCreate(PlayerDefaultData player,HotbarRuntimeData hotbar,ItemListRuntimeData itemList)
        {
            return new PlayerRuntimeData(player, hotbar, itemList);
        }
    }
}
