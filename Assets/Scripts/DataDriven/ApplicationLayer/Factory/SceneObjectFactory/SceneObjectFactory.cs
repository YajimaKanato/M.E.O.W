using UnityEngine;

namespace DataDriven
{
    /// <summary>シーン上のオブジェクトの生成を司るベースクラス</summary>
    public abstract class SceneObjectFactory : MonoBehaviour
    {
        [SerializeField] SceneDataCreateFlow[] _dataFlow;
        protected RuntimeDataRepository _repository;

        public SceneDataCreateFlow[] DataFlow => _dataFlow;

        /// <summary>
        /// シーン上のオブジェクトを生成する関数
        /// </summary>
        /// <param name="repository">ランタイムデータの保管庫</param>
        public abstract void CreateSceneObject(RuntimeDataRepository repository);

        /// <summary>
        /// オブジェクト作成関数
        /// </summary>
        /// <typeparam name="TMono">シーン上のオブジェクトの型</typeparam>
        /// <typeparam name="TRuntime">ランタイムデータの型</typeparam>
        /// <param name="id">ID</param>
        /// <param name="mono">シーン上のオブジェクト</param>
        protected void ObjectCreate<TMono, TRuntime>(DataID id, TMono mono) where TMono : IMono<TRuntime> where TRuntime : IRuntime
        {
            if (_repository.TryGetData<TRuntime>(id, out var data))
            {
                mono.Init(data);
                Debug.Log($"Connect => {typeof(TMono)}");
            }
        }
    }
}
