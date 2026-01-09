using UnityEngine;

namespace DataDriven
{
    /// <summary>インゲーム1のデータ生成を司るクラス</summary>
    [CreateAssetMenu(fileName = "Ingame1Flow", menuName = "DataFlow/Ingame1Flow")]
    public class Ingame1DataCreateFlow : SceneDataCreateFlow
    {
        [Header("DefaultData")]
        [SerializeField] PlayerDefaultData _player;
        [SerializeField] HotbarDefaultData _hotbar;
        [SerializeField] ItemListDefaultData _itemList;
        [SerializeField] DogDefaultData _dog;

        public override void CreateSceneData(RuntimeDataRepository repository)
        {
            _repository = repository;
            DataCreate(DataID.Player, _player, data => new PlayerRuntimeData(data));
            DataCreate(DataID.Dog, _dog, data => new DogRuntimeData(data));
            DataCreate(DataID.ItemList, _itemList, data => new ItemListRuntimeData(data));
            DataCreate(DataID.Hotbar, _hotbar, data => new HotbarRuntimeData(data));
        }
    }
}
