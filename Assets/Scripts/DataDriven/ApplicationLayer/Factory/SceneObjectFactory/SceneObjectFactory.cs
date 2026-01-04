using DataDriven;
using UnityEngine;

namespace DataDriven
{
    /// <summary>シーン上のオブジェクトの生成を司るベースクラス</summary>
    public abstract class SceneObjectFactory : MonoBehaviour
    {
        protected RuntimeDataRepository _repository;

        /// <summary>
        /// シーン上のオブジェクトを生成する関数
        /// </summary>
        /// <param name="repository">ランタイムデータの保管庫</param>
        public abstract void CreateSceneObject(RuntimeDataRepository repository);
    }
}
