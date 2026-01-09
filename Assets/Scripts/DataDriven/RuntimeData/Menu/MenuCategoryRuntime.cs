using UnityEngine;

namespace DataDriven
{
    /// <summary>メニュー項目のランタイムベースデータ</summary>
    public abstract class MenuCategoryRuntime
    {
        /// <summary>
        /// 項目を選択する関数
        /// </summary>
        /// <param name="move">選択する方向</param>
        public abstract void SelectCategory(SlotMoveVertical move);
    }
}
