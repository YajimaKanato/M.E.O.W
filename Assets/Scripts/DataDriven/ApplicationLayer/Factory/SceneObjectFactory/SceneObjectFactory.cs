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

        /// <summary>
        /// プレイヤー作成関数
        /// </summary>
        /// <param name="player">シーン上のプレイヤー</param>
        protected void PlayerCreate(PlayerMono player)
        {
            if (_repository.TryGetData<PlayerRuntimeData>((int)EntityID.Player, out var data))
            {
                player.Init(data);
                Debug.Log("Player was Created");
            }
        }

        /// <summary>
        /// 犬作成関数
        /// </summary>
        /// <param name="dog">シーン上の犬</param>
        protected void DogCreate(DogMono dog)
        {
            if (_repository.TryGetData<DogRuntimeData>((int)EntityID.Dog, out var data))
            {
                dog.Init(data);
                Debug.Log("Dog was Created");
            }
        }
    }
}
