using UnityEngine;

namespace DataDriven
{
    /// <summary>プレイヤーの入力受付を司るクラス</summary>
    public class PlayerInput
    {
        #region 直接Viewに関わらない入力
        /// <summary>
        /// インタラクトを行う関数
        /// Tキー/Aボタンに対応
        /// </summary>
        public void Interact()
        {

        }

        /// <summary>
        /// 意思決定をする関数
        /// Eキー、エンターキー/Aボタンに対応
        /// </summary>
        public void Confirm()
        {

        }

        /// <summary>
        /// キャンセルをする関数
        /// Cキー/Bボタンに対応
        /// </summary>
        public void Cancel()
        {

        }

        /// <summary>
        /// アイテムを使用する関数
        /// Eキー/LTボタンに対応
        /// </summary>
        public void UseItem()
        {

        }

        /// <summary>
        /// ホットバーのスロットを選択する関数
        /// 1～6キー/LRボタンに対応
        /// </summary>
        public void HotbarSelect()
        {

        }

        /// <summary>
        /// アイテムリストの要素を選択する関数
        /// 十字キー、WASDキー/左スティック、DPadに対応
        /// </summary>
        public void ItemListSelect()
        {

        }
        #endregion
    }
}
