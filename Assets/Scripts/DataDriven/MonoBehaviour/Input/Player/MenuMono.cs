using UnityEngine;
using UnityEngine.InputSystem;

namespace DataDriven
{
    /// <summary>メニューの入力処理を司るクラス</summary>
    public class MenuMono : SceneEntity
    {
        MenuFlow _menuFlow;
        MenuInput _menuInput;

        public override void Init()
        {
            _menuFlow = FindFirstObjectByType<MenuFlow>();
            _menuInput = FindFirstObjectByType<MenuInput>();
            ActionRegister();
        }

        public override void Remove()
        {
            ActionUnRegister();
        }

        /// <summary>
        /// アクションを登録する関数
        /// </summary>
        void ActionRegister()
        {
            if (_menuInput)
            {
                _menuInput.RegisterActForStarted(_menuInput.MenuSelectActOnMenu, MenuSelectForKeyboard);
                _menuInput.RegisterActForStarted(_menuInput.SlotNextActOnMenu, MenuSelectNextForGamePad);
                _menuInput.RegisterActForStarted(_menuInput.SlotBackActOnMenu, MenuSelectBackForGamePad);
                _menuInput.RegisterActForStarted(_menuInput.CancelActOnMenu, MenuClose);
                _menuInput.RegisterActForStarted(_menuInput.SelectDownActOnMenu, MenuCategoryDown);
                _menuInput.RegisterActForStarted(_menuInput.SelectUpActOnMenu, MenuCategoryUp);
                _menuInput.RegisterActForStarted(_menuInput.SelectLeftActOnMenu, MenuCategoryElementChangeLeft);
                _menuInput.RegisterActForStarted(_menuInput.SelectRightActOnMenu, MenuCategoryElementChangeRight);
                _menuInput.RegisterActForStarted(_menuInput.EnterActOnMenu, PushEnter);
            }
        }

        void ActionUnRegister()
        {
            if (_menuInput)
            {
                _menuInput.UnRegisterActForStarted(_menuInput.MenuSelectActOnMenu, MenuSelectForKeyboard);
                _menuInput.UnRegisterActForStarted(_menuInput.SlotNextActOnMenu, MenuSelectNextForGamePad);
                _menuInput.UnRegisterActForStarted(_menuInput.SlotBackActOnMenu, MenuSelectBackForGamePad);
                _menuInput.UnRegisterActForStarted(_menuInput.CancelActOnMenu, MenuClose);
                _menuInput.UnRegisterActForStarted(_menuInput.SelectDownActOnMenu, MenuCategoryDown);
                _menuInput.UnRegisterActForStarted(_menuInput.SelectUpActOnMenu, MenuCategoryUp);
                _menuInput.UnRegisterActForStarted(_menuInput.SelectLeftActOnMenu, MenuCategoryElementChangeLeft);
                _menuInput.UnRegisterActForStarted(_menuInput.SelectRightActOnMenu, MenuCategoryElementChangeRight);
                _menuInput.UnRegisterActForStarted(_menuInput.EnterActOnMenu, PushEnter);
            }
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
            _menuFlow.MenuSelectForKeyboard(int.Parse(key) - 1);
        }

        /// <summary>
        /// メニュー項目を選択する関数
        /// LRボタンに対応
        /// </summary>
        void MenuSelectNextForGamePad(InputAction.CallbackContext context)
        {
            _menuFlow.MenuSelectForGamePad(IndexMove.Next);
        }

        /// <summary>
        /// メニュー項目を選択する関数
        /// LRボタンに対応
        /// </summary>
        void MenuSelectBackForGamePad(InputAction.CallbackContext context)
        {
            _menuFlow.MenuSelectForGamePad(IndexMove.Back);
        }

        /// <summary>
        /// メニュー項目内のカテゴリーを切り替える関数
        /// Wキー、上矢印/スティック上、十字キー上に対応
        /// </summary>
        void MenuCategoryUp(InputAction.CallbackContext context)
        {
            _menuFlow.MenuCategorySelect(IndexMove.Back);
        }

        /// <summary>
        /// メニュー項目内のカテゴリーを切り替える関数
        /// Sキー、下矢印/スティック下、十字キー下に対応
        /// </summary>
        void MenuCategoryDown(InputAction.CallbackContext context)
        {
            _menuFlow.MenuCategorySelect(IndexMove.Next);
        }

        /// <summary>
        /// メニュー項目内のカテゴリーを切り替える関数
        /// Aキー、左矢印/スティック左、十字キー左に対応
        /// </summary>
        void MenuCategoryElementChangeLeft(InputAction.CallbackContext context)
        {
            _menuFlow.MenuCategoryElementSelect(IndexMove.Back);
        }

        /// <summary>
        /// メニュー項目内のカテゴリーを切り替える関数
        /// Dキー、右矢印/スティック右、十字キー右に対応
        /// </summary>
        void MenuCategoryElementChangeRight(InputAction.CallbackContext context)
        {
            _menuFlow.MenuCategoryElementSelect(IndexMove.Next);
        }

        /// <summary>
        /// エンター入力で呼ばれる関数
        /// </summary>
        void PushEnter(InputAction.CallbackContext context)
        {
            _menuFlow.MenuPushEnter();
        }

        /// <summary>
        /// メニューを閉じる関数
        /// </summary>
        void MenuClose(InputAction.CallbackContext context)
        {
            _menuFlow.MenuClose();
        }
    }
}
