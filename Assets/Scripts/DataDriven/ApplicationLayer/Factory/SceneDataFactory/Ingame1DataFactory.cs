using UnityEngine;

namespace DataDriven
{
    /// <summary>インゲーム1のデータ生成を司るクラス</summary>
    [CreateAssetMenu(fileName = "Ingame1Factory", menuName = "Factory/Ingame1Factory")]
    public class Ingame1DataFactory : SceneDataFactory
    {
        [Header("DefaultData")]
        [SerializeField] PlayerDefaultData _player;
        [SerializeField] ItemListDefaultData _itemList;
        [SerializeField] DogDefaultData _dog;

        public override void CreateSceneData(RuntimeDataRepository repository)
        {
            _repository = repository;
            PlayerCreate((int)EntityID.Player);
            DogCreate((int)EntityID.Dog);
        }

        /// <summary>
        /// プレイヤー作成関数
        /// </summary>
        /// <param name="id">ID</param>
        void PlayerCreate(int id)
        {
            if (_repository.TryGetData<PlayerRuntimeData>(id, out var dummy)) return;

            //Factory生成
            var playerFactory = new PlayerRuntimeDataFactory();
            var hotbarFactory = new HotbarRuntimeDataFactory();
            var itemListFactory = new ItemListRuntimeDataFactory();

            //データ生成
            var player = playerFactory.PlayerCreate(_player, hotbarFactory.HotbarCreate(), itemListFactory.ItemListCreate(_itemList));

            //保管庫に登録
            _repository.RegisterData(id, player);
            Debug.Log("PlayerData was Created");
        }

        /// <summary>
        /// 犬作成関数
        /// </summary>
        /// <param name="id">ID</param>
        void DogCreate(int id)
        {
            if (_repository.TryGetData<DogRuntime>(id, out var dummy)) return;

            //Factory生成
            var dogFactory = new DogRuntimeDataFactory();

            //データ生成
            var dog = dogFactory.DogCreate(_dog);

            //保管庫に登録
            _repository.RegisterData(id, dog);
            Debug.Log("DogData was Created");
        }
    }
}
