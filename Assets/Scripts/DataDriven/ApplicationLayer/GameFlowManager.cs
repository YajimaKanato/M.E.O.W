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
        PlaySceneSystem _playSceneSystem;

        private void Awake()
        {
            GameStart();
        }

        /// <summary>
        /// ゲームが開始した時に呼ばれる関数
        /// </summary>
        public void GameStart()
        {
            _input.Init();
            _repository = new RuntimeDataRepository();
            _interactSystem = new InteractSystem(_repository);
            _playSceneSystem = new PlaySceneSystem(_repository);
            _dataFactory.CreateSceneData(_repository);
            _objectFactory.CreateSceneObject(_repository);
        }

        #region PlayScene
        /// <summary>
        /// インタラクトが行われたときに呼ばれる関数
        /// </summary>
        /// <param name="character">インタラクトを行う対象のキャラクター</param>
        public void Interact(CharacterRuntime character)
        {
            if (_interactSystem.StartInteract(character)) _input.ChangeActionMap(ActionMapName.UI);
        }

        /// <summary>
        /// アイテムを選択する関数
        /// </summary>
        /// <param name="index">選択するスロット</param>
        public void HotbarSelectForKeyboard(int index)
        {
            _playSceneSystem.HotbarSelectForKetboard(index);
        }

        /// <summary>
        /// アイテムを選択する関数
        /// </summary>
        /// <param name="dir">選択するスロットをずらす方向</param>
        public void HotbarselectForGamePad(int dir)
        {
            _playSceneSystem.HotbarSelectForGamePad(dir);
        }

        public void UseItem()
        {
            _playSceneSystem.UseItem();
        }
        #endregion

        #region UI
        /// <summary>
        /// インタラクトを進める時に呼ばれる関数
        /// </summary>
        public void Confirm()
        {
            if (_interactSystem.PushInteract()) _input.ChangeActionMap(ActionMapName.Player);
        }

        /// <summary>
        /// アイテムを選択する関数
        /// </summary>
        /// <param name="index">選択するスロット</param>
        public void HotbarSelectOnConversationForKeyboard(int index)
        {
            _interactSystem.HotbarSelectForKetboard(index);
        }

        /// <summary>
        /// アイテムを選択する関数
        /// </summary>
        /// <param name="dir">選択するスロットをずらす方向</param>
        public void HotbarselectOnConversationForGamePad(int dir)
        {
            _interactSystem.HotbarSelectForGamePad(dir);
        }
        #endregion
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
