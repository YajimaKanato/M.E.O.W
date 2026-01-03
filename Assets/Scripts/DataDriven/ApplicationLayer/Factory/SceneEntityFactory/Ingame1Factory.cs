using UnityEngine;

namespace DataDriven
{
    /// <summary>インゲーム1のエンティティ生成を司るクラス</summary>
    [CreateAssetMenu(fileName = "Ingame1Factory", menuName = "Factory/Ingame1Factory")]
    public class Ingame1Factory : SceneEntityFactory
    {
        [SerializeField] PlayerDefaultData _player;
        [SerializeField] ItemListDefaultData _itemList;

        public override void CreateSceneEntity(RuntimeDataRepository repository)
        {
            //Factory生成
            var playerFactory = new PlayerRuntimeDataFactory();
            var hotbarFactory = new HotbarRuntimeDataFactory();
            var itemListFactory = new ItemListRuntimeDataFactory();

            //エンティティ生成
            var player = playerFactory.PlayerCreate(_player, hotbarFactory.HotbarCreate(), itemListFactory.ItemListCreate(_itemList));

            //保管庫に登録
            repository.RegisterData(0, player);
        }
    }
}
