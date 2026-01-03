using UnityEngine;

namespace DataDriven
{
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
