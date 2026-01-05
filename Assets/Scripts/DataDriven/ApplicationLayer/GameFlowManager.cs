using UnityEngine;

namespace DataDriven
{
    /// <summary>ゲームの流れを司るクラス</summary>
    public class GameFlowManager : MonoBehaviour
    {
        [SerializeField] InputManager _input;
        [SerializeField] SceneDataFactory _dataFactory;
        [SerializeField] SceneObjectFactory _objectFactory;
        RuntimeDataRepository _repository;
        InteractSystem _interactSystem;

        private void Awake()
        {
            _input.Init();
            _repository = new RuntimeDataRepository();
            _interactSystem = new InteractSystem(_repository);
            GameStart();
        }

        /// <summary>
        /// ゲームが開始した時に呼ばれる関数
        /// </summary>
        public void GameStart()
        {
            _dataFactory.CreateSceneData(_repository);
            _objectFactory.CreateSceneObject(_repository);
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

    /// <summary>アクションマップの名前</summary>
    public enum ActionMapName
    {
        Player,
        UI,
        OutGame,
        Unknown
    }
}
