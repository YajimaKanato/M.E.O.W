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
}
