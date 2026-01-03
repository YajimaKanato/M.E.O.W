using UnityEngine;

namespace DataDriven
{
    /// <summary>ゲームの流れを司るクラス</summary>
    public class GameFlowManager : MonoBehaviour
    {
        [SerializeField] SceneEntityFactory _factory;
        RuntimeDataRepository _repository;
        InteractSystem _interactSystem;

        private void Awake()
        {
            _repository = new RuntimeDataRepository();
            _interactSystem = new InteractSystem();
        }

        public GameFlowManager(
            SceneEntityFactory factory,
            RuntimeDataRepository repository,
            InteractSystem interactSystem)
        {
            _factory = factory;
            _repository = repository;
            _interactSystem = interactSystem;
        }

        /// <summary>
        /// ゲームが開始した時に呼ばれる関数
        /// </summary>
        public void GameStart()
        {
            _factory.CreateSceneEntity(_repository);
        }

        /// <summary>
        /// インタラクトが行われたときに呼ばれる関数
        /// </summary>
        /// <param name="character">インタラクトを行う対象のキャラクター</param>
        public void Interact(CharacterRuntime character)
        {
            _interactSystem.StartInteract(character);
        }

        /// <summary>
        /// インタラクトを進める時に呼ばれる関数
        /// </summary>
        public void Confirm()
        {
            _interactSystem.PushInteract();
        }
    }
}
