using UnityEngine;

namespace DataDriven
{
    /// <summary>プレイヤーのランタイムデータの作成を司るクラス</summary>
    public class PlayerRuntimeDataFactory
    {
        /// <summary>
        /// プレイヤーのランタイムデータを作成する関数
        /// </summary>
        /// <returns>プレイヤーのランタイムデータ</returns>
        public PlayerRuntimeData PlayerCreate(PlayerDefaultData player,HotbarRuntimeData hotbar,ItemListRuntimeData itemList)
        {
            return new PlayerRuntimeData(player, hotbar, itemList);
        }
    }

    /// <summary>ホットバーのランタイムデータの作成を司るクラス</summary>
    public class HotbarRuntimeDataFactory
    {
        /// <summary>
        /// ホットバーのランタイムデータを作成する関数
        /// </summary>
        /// <returns>ホットバーのランタイムデータ</returns>
        public HotbarRuntimeData HotbarCreate()
        {
            return new HotbarRuntimeData();
        }
    }

    /// <summary>アイテムリストのランタイムデータの作成を司るクラス</summary>
    public class ItemListRuntimeDataFactory
    {
        /// <summary>
        /// アイテムリストのランタイムデータを作成する関数
        /// </summary>
        /// <param name="itemList">アイテムリストの初期データ</param>
        /// <returns>アイテムリストのランタイムデータ</returns>
        public ItemListRuntimeData ItemListCreate(ItemListDefaultData itemList)
        {
            return new ItemListRuntimeData(itemList);
        }
    }
}
