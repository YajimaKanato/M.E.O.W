using UnityEngine;
using UnityEngine.InputSystem;

namespace DataDriven
{
    /// <summary>メニューの入力処理を司るクラス</summary>
    public class MenuMono : MonoBehaviour, IMono<MenuRuntimeData>
    {
        InputManager _inputManager;
        GameFlowManager _gameFlowManager;
        static MenuMono _instance;

        public void Awake()
        {
            //Init();
        }

        public void Init(MenuRuntimeData runtime)
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
            _inputManager.RegisterAct(_inputManager.MenuActOnPlayScene, MenuOpen);
            _inputManager.RegisterAct(_inputManager.MenuActOnUI, MenuOpen);
            _inputManager.RegisterAct(_inputManager.MenuSelectActOnMenu, MenuSelectForKeyboard);
            _inputManager.RegisterAct(_inputManager.SlotNextActOnMenu, MenuSelectNextForGamePad);
            _inputManager.RegisterAct(_inputManager.SlotBackActOnMenu, MenuSelectBackForGamePad);
            _inputManager.RegisterAct(_inputManager.CancelActOnMenu, MenuClose);
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
            _gameFlowManager.MenuSelectForGamePad(1);
        }

        /// <summary>
        /// メニュー項目を選択する関数
        /// LRボタンに対応
        /// </summary>
        void MenuSelectBackForGamePad(InputAction.CallbackContext context)
        {
            _gameFlowManager.MenuSelectForGamePad(-1);
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
