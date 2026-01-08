using UnityEngine;

namespace DataDriven
{
    /// <summary>インゲーム1のオブジェクト生成を司るクラス</summary>
    public class Ingame1ObjectFactory : SceneObjectFactory
    {
        [Header("SceneObject")]
        [SerializeField] PlayerMono _player;
        [SerializeField] DogMono _dog;
        [SerializeField] MenuMono _menu;

        public override void CreateSceneObject(RuntimeDataRepository repository)
        {
            _repository = repository;

            if (_player) ObjectCreate<PlayerMono, PlayerRuntimeData>((int)EntityID.Player, _player);
            if (_dog) ObjectCreate<DogMono, DogRuntimeData>((int)EntityID.Dog, _dog);
            if (_menu) ObjectCreate<MenuMono, MenuRuntimeData>((int)EntityID.Menu, _menu);
        }
    }
}
