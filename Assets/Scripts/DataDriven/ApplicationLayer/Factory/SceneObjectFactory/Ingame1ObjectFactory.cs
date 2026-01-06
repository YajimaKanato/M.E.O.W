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

            if (_player) PlayerCreate(_player);
            if (_dog) DogCreate(_dog);
        }
    }
}
