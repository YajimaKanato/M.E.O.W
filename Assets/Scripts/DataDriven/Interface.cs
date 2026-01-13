using UnityEngine;

namespace DataDriven
{
    /// <summary>ランタイムデータのラベル付け用インターフェース</summary>
    public interface IRuntime { }

    /// <summary>初期データのラベル付け用インターフェース</summary>
    public interface IDefault { }

    /// <summary>シーン上のオブジェクト</summary>
    /// <typeparam name="T">ランタイムデータの型</typeparam>
    public interface IMono<T> where T : IRuntime
    {
        /// <summary>初期化関数</summary>
        /// <param name="runtime">ランタイムデータ</param>
        public void Init(T runtime);
    }

    /// <summary>シーン上のオブジェクトのラベル付け用インターフェース</summary>
    public interface IMono
    {
        /// <summary>IDを返すプロパティ</summary>
        public DataID ID { get; }

        /// <summary>初期化関数</summary>
        /// <param name="connector">Unityと接続するクラス</param>
        public void Init(UnityConnector connector);
    }

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
}
