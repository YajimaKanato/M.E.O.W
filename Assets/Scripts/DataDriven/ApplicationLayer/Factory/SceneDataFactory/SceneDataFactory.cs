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

        /// <summary>
        /// プレイヤー作成関数
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="data">プレイヤーの初期データ</param>
        protected void PlayerCreate(int id, PlayerDefaultData data)
        {
            if (_repository.TryGetData<PlayerRuntimeData>(id, out var dummy)) return;

            //Factory生成
            var playerFactory = new PlayerRuntimeDataFactory();

            //データ生成
            if (!_repository.TryGetData<ItemListRuntimeData>((int)EntityID.ItemList, out var itemList)) return;
            if (!_repository.TryGetData<HotbarRuntimeData>((int)EntityID.Hotbar, out var hotbar)) return;
            var player = playerFactory.PlayerCreate(data, hotbar, itemList);

            //保管庫に登録
            _repository.RegisterData(id, player);
            Debug.Log("PlayerData was Created");
        }

        /// <summary>
        /// メニュー作成関数
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="data">メニューの初期データ</param>
        protected void MenuCreate(int id, MenuDefaultData data)
        {
            if (_repository.TryGetData<MenuRuntimeData>(id, out var dummy)) return;

            //Factory生成
            var menuFactory = new MenuRuntimeFactory();

            //データ生成
            if (!_repository.TryGetData<ItemListRuntimeData>((int)EntityID.ItemList, out var itemList)) return;
            var menu = menuFactory.MenuCreate(data, itemList);

            //保管庫に登録
            _repository.RegisterData(id, menu);
            Debug.Log("Menu was Created");
        }

        /// <summary>
        /// アイテムリスト作成関数
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="data">アイテムリストの初期データ</param>
        protected void ItemListCreate(int id, ItemListDefaultData data)
        {
            if (_repository.TryGetData<ItemListRuntimeData>(id, out var dummy)) return;

            //Factory生成
            var itemListFactory = new ItemListRuntimeDataFactory();

            //データ生成
            var itemList = itemListFactory.ItemListCreate(data);

            //保管庫に登録
            _repository.RegisterData(id, itemList);
            Debug.Log("ItemList was Created");
        }

        /// <summary>
        /// ホットバー作成関数
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="data">ホットバーの初期データ</param>
        protected void HotbarCreate(int id, HotbarDefaultData data)
        {
            if (_repository.TryGetData<HotbarRuntimeData>(id, out var dummy)) return;

            //Factory生成
            var hotbarFactory = new HotbarRuntimeDataFactory();

            //データ生成
            var hotbar = hotbarFactory.HotbarCreate(data);

            //保管庫に登録
            _repository.RegisterData(id, hotbar);
            Debug.Log("ItemList was Created");
        }

        /// <summary>
        /// 犬作成関数
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="data">犬の初期データ</param>
        protected void DogCreate(int id, DogDefaultData data)
        {
            if (_repository.TryGetData<DogRuntime>(id, out var dummy)) return;

            //Factory生成
            var dogFactory = new DogRuntimeDataFactory();

            //データ生成
            var dog = dogFactory.DogCreate(data);

            //保管庫に登録
            _repository.RegisterData(id, dog);
            Debug.Log("DogData was Created");
        }
    }
}


