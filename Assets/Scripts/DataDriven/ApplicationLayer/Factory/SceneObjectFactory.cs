using UnityEngine;

namespace DataDriven
{
    /// <summary>シーン上のオブジェクトの生成を司るベースクラス</summary>
    public class SceneObjectFactory : MonoBehaviour
    {
        [SerializeField] SceneDataCreateFlow[] _dataFlows;
        [SerializeField] SceneEntity[] _objs;
        UnityConnector _connector;

        /// <summary>
        /// シーン上のオブジェクトを生成する関数
        /// </summary>
        /// <param name="repository">ランタイムデータの保管庫</param>
        /// <param name="connector">Unityに接続するクラス</param>
        public void CreateSceneObject(RuntimeDataRepository repository, UnityConnector connector)
        {
            _connector = connector;
            foreach (var flow in _dataFlows)
            {
                flow.CreateSceneData(repository);
            }

            foreach (var obj in _objs)
            {
                ObjectCreate(obj);
            }
        }

        /// <summary>
        /// オブジェクト作成関数
        /// </summary>
        /// <typeparam name="TMono">シーン上のオブジェクトの型</typeparam>
        /// <param name="mono">シーン上のオブジェクト</param>
        protected void ObjectCreate<TMono>(TMono mono) where TMono : IMono
        {
            mono.Init(_connector);
            Debug.Log($"Connect => {typeof(TMono)}");
        }
    }
}
