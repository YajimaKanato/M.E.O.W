using UnityEngine;

namespace DataDriven
{
    /// <summary>ランタイムデータのラベル付け用インターフェース</summary>
    public interface IRuntime { }

    /// <summary>横方向の入力を受け付けるものに実装するインターフェース</summary>
    public interface IHorizontalArrowInput
    {
        /// <summary>
        /// 項目を選択する関数
        /// </summary>
        /// <param name="move">選択する方向</param>
        public void SelectCategory(IndexMove move);
    }

    /// <summary>縦方向の入力を受け付けるものに実装するインターフェース</summary>
    public interface IVerticalArrowInput
    {
        /// <summary>
        /// 項目を選択する関数
        /// </summary>
        /// <param name="move">選択する方向</param>
        public void SelectCategory(IndexMove move);
    }

    /// <summary>アイテムコレクションの要素</summary>
    public interface IItemCollection
    {
        /// <summary>アイテムの情報を返すプロパティ</summary>
        public KeyItemDefaultData ItemInfo { get; }

        /// <summary>アイテムを獲得しているかどうかを返すプロパティ</summary>
        public bool IsObtained { get; }

        /// <summary>アイテムを獲得する関数</summary>
        public void ObtainItem();
    }
}
