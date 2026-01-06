using UnityEngine;

namespace DataDriven
{
    /// <summary>データの生成を司るベースクラス</summary>
    public abstract class SceneDataFactory : ScriptableObject
    {
        protected RuntimeDataRepository _repository;

        /// <summary>
        /// データ生成を行う関数
        /// </summary>
        /// <param name="repository">ランタイムデータの保管庫</param>
        public abstract void CreateSceneData(RuntimeDataRepository repository);
    }
}


