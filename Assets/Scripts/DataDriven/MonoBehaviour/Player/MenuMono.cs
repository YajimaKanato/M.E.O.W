using UnityEngine;
using UnityEngine.InputSystem;

namespace DataDriven
{
    /// <summary>メニューの入力処理を司るクラス</summary>
    public class MenuMono : SceneEntity
    {
        InputManager _inputManager;
        GameFlowManager _gameFlowManager;
        static MenuMono _instance;

        public void Awake()
        {
            //Init();
        }

        public override void Init(UnityConnector connector)
        {
            if (!_instance)
            {
                _instance = this;
                _gameFlowManager = FindFirstObjectByType<GameFlowManager>();
                _inputManager = FindFirstObjectByType<InputManager>();
                if (_inputManager) ActionRegister();
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// アクションを登録する関数
        /// </summary>
        void ActionRegister()
        {
            _inputManager.RegisterActForStarted(_inputManager.MenuActOnPlayScene, MenuOpen);
            _inputManager.RegisterActForStarted(_inputManager.MenuActOnUI, MenuOpen);
            _inputManager.RegisterActForStarted(_inputManager.MenuSelectActOnMenu, MenuSelectForKeyboard);
            _inputManager.RegisterActForStarted(_inputManager.SlotNextActOnMenu, MenuSelectNextForGamePad);
            _inputManager.RegisterActForStarted(_inputManager.SlotBackActOnMenu, MenuSelectBackForGamePad);
            _inputManager.RegisterActForStarted(_inputManager.CancelActOnMenu, MenuClose);
            _inputManager.RegisterActForStarted(_inputManager.SelectDownActOnMenu, MenuCategoryDown);
            _inputManager.RegisterActForStarted(_inputManager.SelectUpActOnMenu, MenuCategoryUp);
            _inputManager.RegisterActForStarted(_inputManager.SelectLeftActOnMenu, MenuCategoryElementChangeLeft);
            _inputManager.RegisterActForStarted(_inputManager.SelectRightActOnMenu, MenuCategoryElementChangeRight);
            _inputManager.RegisterActForStarted(_inputManager.EnterActOnMenu, PushEnter);
        }

        /// <summary>
        /// メニューを開く関数
        /// </summary>
        void MenuOpen(InputAction.CallbackContext context)
        {
            _gameFlowManager.MenuOpen();
        }

        /// <summary>
        /// メニュー項目を選択する関数
        /// 1～4キーに対応
        /// </summary>
        void MenuSelectForKeyboard(InputAction.CallbackContext context)
        {
            var key = context.control.name;
            if (key.Length > 1)
            {
                key = key.Substring(key.Length - 1);
            }
            Debug.Log(key);
            _gameFlowManager.MenuSelectForKeyboard(int.Parse(key) - 1);
        }

        /// <summary>
        /// メニュー項目を選択する関数
        /// LRボタンに対応
        /// </summary>
        void MenuSelectNextForGamePad(InputAction.CallbackContext context)
        {
            _gameFlowManager.MenuSelectForGamePad(IndexMove.Next);
        }

        /// <summary>
        /// メニュー項目を選択する関数
        /// LRボタンに対応
        /// </summary>
        void MenuSelectBackForGamePad(InputAction.CallbackContext context)
        {
            _gameFlowManager.MenuSelectForGamePad(IndexMove.Back);
        }

        /// <summary>
        /// メニュー項目内のカテゴリーを切り替える関数
        /// Wキー、上矢印/スティック上、十字キー上に対応
        /// </summary>
        void MenuCategoryUp(InputAction.CallbackContext context)
        {
            _gameFlowManager.MenuCategorySelect(IndexMove.Back);
        }

        /// <summary>
        /// メニュー項目内のカテゴリーを切り替える関数
        /// Sキー、下矢印/スティック下、十字キー下に対応
        /// </summary>
        void MenuCategoryDown(InputAction.CallbackContext context)
        {
            _gameFlowManager.MenuCategorySelect(IndexMove.Next);
        }

        /// <summary>
        /// メニュー項目内のカテゴリーを切り替える関数
        /// Aキー、左矢印/スティック左、十字キー左に対応
        /// </summary>
        void MenuCategoryElementChangeLeft(InputAction.CallbackContext context)
        {
            _gameFlowManager.MenuCategoryElementSelect(IndexMove.Back);
        }

        /// <summary>
        /// メニュー項目内のカテゴリーを切り替える関数
        /// Dキー、右矢印/スティック右、十字キー右に対応
        /// </summary>
        void MenuCategoryElementChangeRight(InputAction.CallbackContext context)
        {
            _gameFlowManager.MenuCategoryElementSelect(IndexMove.Next);
        }

        /// <summary>
        /// エンター入力で呼ばれる関数
        /// </summary>
        void PushEnter(InputAction.CallbackContext context)
        {
            _gameFlowManager.MenuPushEnter();
        }

        /// <summary>
        /// メニューを閉じる関数
        /// </summary>
        void MenuClose(InputAction.CallbackContext context)
        {
            _gameFlowManager.MenuClose();
        }
    }
}
