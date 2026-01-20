using UnityEngine;

namespace DataDriven
{
    /// <summary>ランタイムデータの作成フローを管理するクラス</summary>
    public abstract class RuntimeBuilderBase : ScriptableObject
    {
        /// <summary>ランタイムデータを作成する関数</summary>
        /// <param name="repository">データの保管庫</param>
        /// <param name="id">ID</param>
        public abstract bool CreateRuntime(RuntimeDataRepository repository, DataID id);
    }
}
