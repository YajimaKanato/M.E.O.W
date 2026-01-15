using UnityEngine;
using UnityEngine.InputSystem;

namespace DataDriven
{
    public class TitleMono : SceneEntity
    {
        InputManager _inputManager;
        GameFlowManager _gameFlowManager;

        public override void Init(UnityConnector connector)
        {
            _gameFlowManager = FindFirstObjectByType<GameFlowManager>();
            _inputManager = FindFirstObjectByType<InputManager>();
            if (_inputManager) ActionRegister();
            DontDestroyOnLoad(gameObject);
        }

        void ActionRegister()
        {
            //アウトゲーム
            _inputManager.RegisterActForStarted(_inputManager.EnterActOnOutGame, OpenCategory);
            _inputManager.RegisterActForStarted(_inputManager.SelectUpOnOutGame, SelectBackCategory);
            _inputManager.RegisterActForStarted(_inputManager.SelectDownOnOutGame, SelectNextCategory);
            //アウトゲームカテゴリー
            _inputManager.RegisterActForStarted(_inputManager.MenuSelectActOnOutGameCategory, CategorySelectForKeyboard);
            _inputManager.RegisterActForStarted(_inputManager.MenuNextActOnOutGameCategory, CategorySelectNextForGamePad);
            _inputManager.RegisterActForStarted(_inputManager.MenuBackActOnOutGameCategory, CategorySelectBackForGamePad);
            _inputManager.RegisterActForStarted(_inputManager.SelectUpOnOutGameCategory, TitleCategoryUp);
            _inputManager.RegisterActForStarted(_inputManager.SelectDownOnOutGameCategory, TitleCategoryDown);
            _inputManager.RegisterActForStarted(_inputManager.SelectLeftOnOutGameCategory, TitleCategoryElementChangeLeft);
            _inputManager.RegisterActForStarted(_inputManager.SelectRightOnOutGameCategory, TitleCategoryElementChangeRight);
            _inputManager.RegisterActForStarted(_inputManager.EnterActOnOutGameCategory, PushEnter);
            _inputManager.RegisterActForStarted(_inputManager.CancelActOnOutGameCategory, CloseCategory);
        }

        #region Title
        void OpenCategory(InputAction.CallbackContext context)
        {
            _gameFlowManager.OpenCategory();
        }

        void SelectNextCategory(InputAction.CallbackContext context)
        {
            _gameFlowManager.SelectCategory(IndexMove.Next);
        }

        void SelectBackCategory(InputAction.CallbackContext context)
        {
            _gameFlowManager.SelectCategory(IndexMove.Back);
        }
        #endregion

        #region TitleCategory
        /// <summary>
        /// タイトル項目を選択する関数
        /// 1～4キーに対応
        /// </summary>
        void CategorySelectForKeyboard(InputAction.CallbackContext context)
        {
            var key = context.control.name;
            if (key.Length > 1)
            {
                key = key.Substring(key.Length - 1);
            }
            Debug.Log(key);
            _gameFlowManager.TitleSelectForKeyboard(int.Parse(key) - 1);
        }

        /// <summary>
        /// タイトル項目を選択する関数
        /// LRボタンに対応
        /// </summary>
        void CategorySelectNextForGamePad(InputAction.CallbackContext context)
        {
            _gameFlowManager.TitleSelectForGamePad(IndexMove.Next);
        }

        /// <summary>
        /// タイトル項目を選択する関数
        /// LRボタンに対応
        /// </summary>
        void CategorySelectBackForGamePad(InputAction.CallbackContext context)
        {
            _gameFlowManager.TitleSelectForGamePad(IndexMove.Back);
        }

        /// <summary>
        /// タイトル項目内のカテゴリーを切り替える関数
        /// Wキー、上矢印/スティック上、十字キー上に対応
        /// </summary>
        void TitleCategoryUp(InputAction.CallbackContext context)
        {
            _gameFlowManager.TitleCategorySelect(IndexMove.Back);
        }

        /// <summary>
        /// タイトル項目内のカテゴリーを切り替える関数
        /// Sキー、下矢印/スティック下、十字キー下に対応
        /// </summary>
        void TitleCategoryDown(InputAction.CallbackContext context)
        {
            _gameFlowManager.TitleCategorySelect(IndexMove.Next);
        }

        /// <summary>
        /// タイトル項目内のカテゴリーを切り替える関数
        /// Aキー、左矢印/スティック左、十字キー左に対応
        /// </summary>
        void TitleCategoryElementChangeLeft(InputAction.CallbackContext context)
        {
            _gameFlowManager.TitleCategoryElementSelect(IndexMove.Back);
        }

        /// <summary>
        /// タイトル項目内のカテゴリーを切り替える関数
        /// Dキー、右矢印/スティック右、十字キー右に対応
        /// </summary>
        void TitleCategoryElementChangeRight(InputAction.CallbackContext context)
        {
            _gameFlowManager.TitleCategoryElementSelect(IndexMove.Next);
        }

        /// <summary>
        /// エンター入力で呼ばれる関数
        /// </summary>
        void PushEnter(InputAction.CallbackContext context)
        {
            _gameFlowManager.TitlePushEnter();
        }

        /// <summary>
        /// カテゴリーを閉じる関数
        /// </summary>
        /// <param name="context"></param>
        void CloseCategory(InputAction.CallbackContext context)
        {
            _gameFlowManager.CloseCategory();
        }
        #endregion
    }
}
