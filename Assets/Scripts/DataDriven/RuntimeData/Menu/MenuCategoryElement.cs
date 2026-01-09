using UnityEngine;

namespace DataDriven
{
    /// <summary>メニュー項目の中身のベースクラス</summary>
    public abstract class MenuCategoryElement : IRuntime
    {
        /// <summary>
        /// 要素を変更する関数
        /// </summary>
        /// <param name="move">変更する方向</param>
        public abstract void ChangeElement(IndexMove move);
    }
}
