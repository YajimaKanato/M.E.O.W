using UnityEngine;

namespace DataDriven
{
    /// <summary>インゲーム1のオブジェクト生成を司るクラス</summary>
    public class Ingame1ObjectFactory : SceneObjectFactory
    {
        [Header("SceneObject")]
        [SerializeField] PlayerMono _player;
        [SerializeField] DogMono _dog;

        public override void CreateSceneObject(RuntimeDataRepository repository)
        {
            _repository = repository;

            if (_player) PlayerCreate();
            if (_dog) DogCreate();
        }

        /// <summary>
        /// プレイヤー作成関数
        /// </summary>
        void PlayerCreate()
        {
            if (_repository.TryGetData<PlayerRuntimeData>((int)EntityID.Player, out var player))
            {
                _player.Init(player);
                Debug.Log("Player was Created");
            }
        }

        /// <summary>
        /// 犬作成関数
        /// </summary>
        void DogCreate()
        {
            if (_repository.TryGetData<DogRuntime>((int)EntityID.Dog, out var dog))
            {
                _dog.Init(dog);
                Debug.Log("Dog was Created");
            }
        }
    }
}
