using UnityEngine;

namespace DataDriven
{
    /// <summary>ゲームの流れを司るクラス</summary>
    public class GameFlowManager : MonoBehaviour
    {
        InputManager _input;
        SceneDataCreateFlow[] _dataFlow;
        SceneObjectFactory _objectFactory;
        RuntimeDataRepository _repository;
        InteractSystem _interactSystem;
        PlaySceneSystem _playSceneSystem;
        MenuSystem _menuSystem;
        static GameFlowManager _instance;

        private void Awake()
        {
            GameStart();
        }

        /// <summary>
        /// ゲームが開始した時に呼ばれる関数
        /// </summary>
        public void GameStart()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
                //インスタンス生成
                _repository = new RuntimeDataRepository();
                _interactSystem = new InteractSystem(_repository);
                _playSceneSystem = new PlaySceneSystem(_repository);
                _menuSystem = new MenuSystem(_repository);
                _input = FindFirstObjectByType<InputManager>();
                //処理実行
                _input?.Init();
            }
            else
            {
                Destroy(gameObject);
            }
            //インスタンス生成
            _objectFactory = GameObject.FindWithTag("ObjectFactory").GetComponent<SceneObjectFactory>();
            _dataFlow = _objectFactory.DataFlow;
            //処理実行
            foreach (var dataFlow in _dataFlow)
                dataFlow?.CreateSceneData(_repository);
            _objectFactory?.CreateSceneObject(_repository);
        }

        #region PlayScene
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
        public void HotbarselectForGamePad(IndexMove dir)
        {
            _playSceneSystem.HotbarSelectForGamePad(dir);
        }

        public void UseItem()
        {
            _playSceneSystem.UseItem();
        }
        #endregion

        #region Interact
        /// <summary>
        /// インタラクトが行われたときに呼ばれる関数
        /// </summary>
        /// <param name="character">インタラクトを行う対象のキャラクター</param>
        public void Interact(CharacterRuntimeData character)
        {
            if (_interactSystem.StartInteract(character)) _input.ChangeActionMap(ActionMapName.UI);
        }

        /// <summary>
        /// インタラクトを進める時に呼ばれる関数
        /// </summary>
        public void Confirm()
        {
            if (_interactSystem.PushInteract()) _input.ChangeActionMap();
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
        public void HotbarselectOnConversationForGamePad(IndexMove dir)
        {
            _interactSystem.HotbarSelectForGamePad(dir);
        }
        #endregion

        #region Menu
        /// <summary>
        /// メニューを開く関数
        /// </summary>
        public void MenuOpen()
        {
            if (_menuSystem.MenuOpen()) _input.ChangeActionMap(ActionMapName.Menu);
        }

        /// <summary>
        /// メニュー項目を選択する関数
        /// </summary>
        /// <param name="index">選択するスロット</param>
        public void MenuSelectForKeyboard(int index)
        {
            _menuSystem.MenuSelectForKeyboard(index);
        }

        /// <summary>
        /// メニュー項目を選択する関数
        /// </summary>
        /// <param name="dir">選択するスロットをずらす方向</param>
        public void MenuSelectForGamePad(IndexMove dir)
        {
            _menuSystem.MenuSelectForGamePad(dir);
        }

        /// <summary>
        /// メニュー項目内のカテゴリー選択を行う関数
        /// </summary>
        /// <param name="move">スロット選択の方向</param>
        public void MenuCategorySelect(IndexMove move)
        {
            _menuSystem.MenuCategorySelect(move);
        }

        /// <summary>
        /// 要素を変更する関数
        /// </summary>
        /// <param name="move">変更する方向</param>
        public void MenuCategoryElementSelect(IndexMove move)
        {
            _menuSystem.MenuCategoryElementSelect(move);
        }

        /// <summary>
        /// メニューを閉じる関数
        /// </summary>
        public void MenuClose()
        {
            if (_menuSystem.MenuClose()) _input.ChangeActionMap();
        }
        #endregion
    }
}
