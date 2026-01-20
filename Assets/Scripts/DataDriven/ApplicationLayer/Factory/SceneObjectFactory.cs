using UnityEngine;

namespace DataDriven
{
    /// <summary>シーン上のオブジェクトの生成を司るベースクラス</summary>
    public class SceneObjectFactory : MonoBehaviour
    {
        [SerializeField] SceneDataCreateFlow[] _dataFlows;
        [SerializeField] SceneEntity[] _objs;
        [SerializeField] ViewBase[] _views;

        /// <summary>
        /// シーン上のオブジェクトを生成する関数
        /// </summary>
        /// <param name="repository">ランタイムデータの保管庫</param>
        /// <param name="connector">Unityに接続するクラス</param>
        public void CreateSceneObject(RuntimeDataRepository repository, UnityConnector connector)
        {
            foreach (var flow in _dataFlows)
            {
                flow?.CreateSceneData(repository);
            }

            foreach (var obj in _objs)
            {
                obj?.Init();
            }

            foreach (var view in _views)
            {
                view?.Init(connector);
            }
        }

        public void DestroySceneObject()
        {
            foreach (var obj in _objs)
            {
                obj?.Remove();
                Debug.Log($"Destory => {obj.name}");
            }
        }
    }
}
