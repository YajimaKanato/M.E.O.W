using UnityEngine;

namespace DataDriven
{
    /// <summary>インゲーム1のデータ生成を司るクラス</summary>
    [CreateAssetMenu(fileName = "Ingame1Factory", menuName = "Factory/Ingame1Factory")]
    public class Ingame1DataFactory : SceneDataFactory
    {
        [Header("DefaultData")]
        [SerializeField] MenuDefaultData _menu;
        [SerializeField] PlayerDefaultData _player;
        [SerializeField] HotbarDefaultData _hotbar;
        [SerializeField] ItemListDefaultData _itemList;
        [SerializeField] DogDefaultData _dog;

        public override void CreateSceneData(RuntimeDataRepository repository)
        {
            _repository = repository;
            ItemListCreate((int)EntityID.ItemList, _itemList);
            HotbarCreate((int)EntityID.Hotbar, _hotbar);
            MenuCreate((int)EnterType.Menu, _menu);
            PlayerCreate((int)EntityID.Player, _player);
            DogCreate((int)EntityID.Dog, _dog);
        }
    }
}
