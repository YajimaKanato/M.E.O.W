using UnityEngine;

namespace DataDriven
{
    /// <summary>シーン上のエンティティの生成を司るベースクラス</summary>
    public abstract class SceneEntityFactory : ScriptableObject
    {
        /// <summary>
        /// エンティティ生成を行う関数
        /// </summary>
        /// <param name="repository">ランタイムデータの保管庫</param>
        public abstract void CreateSceneEntity(RuntimeDataRepository repository);
    }
}


