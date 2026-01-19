using UnityEngine;
using UnityEngine.InputSystem;

namespace DataDriven
{
    public class TitleMono : SceneEntity
    {
        OutGameFlow _outGameFlow;
        OutGameInput _outGameInput;
        OutGameCategoryInput _outGameCategoryInput;

        public override void Init()
        {
            _outGameFlow = FindFirstObjectByType<OutGameFlow>();
            _outGameInput = FindFirstObjectByType<OutGameInput>();
            _outGameCategoryInput = FindFirstObjectByType<OutGameCategoryInput>();
            if (_outGameInput && _outGameCategoryInput) ActionRegister();
        }

        void ActionRegister()
        {
            if (_outGameInput)
            {
                //アウトゲーム
                _outGameInput.RegisterActForStarted(_outGameInput.EnterActOnOutGame, OpenCategory);
                _outGameInput.RegisterActForStarted(_outGameInput.SelectUpOnOutGame, SelectBackCategory);
                _outGameInput.RegisterActForStarted(_outGameInput.SelectDownOnOutGame, SelectNextCategory);
            }

            if (_outGameCategoryInput)
            {
                //アウトゲームカテゴリー
                _outGameCategoryInput.RegisterActForStarted(_outGameCategoryInput.MenuSelectActOnOutGameCategory, CategorySelectForKeyboard);
                _outGameCategoryInput.RegisterActForStarted(_outGameCategoryInput.MenuNextActOnOutGameCategory, CategorySelectNextForGamePad);
                _outGameCategoryInput.RegisterActForStarted(_outGameCategoryInput.MenuBackActOnOutGameCategory, CategorySelectBackForGamePad);
                _outGameCategoryInput.RegisterActForStarted(_outGameCategoryInput.SelectUpOnOutGameCategory, TitleCategoryUp);
                _outGameCategoryInput.RegisterActForStarted(_outGameCategoryInput.SelectDownOnOutGameCategory, TitleCategoryDown);
                _outGameCategoryInput.RegisterActForStarted(_outGameCategoryInput.SelectLeftOnOutGameCategory, TitleCategoryElementChangeLeft);
                _outGameCategoryInput.RegisterActForStarted(_outGameCategoryInput.SelectRightOnOutGameCategory, TitleCategoryElementChangeRight);
                _outGameCategoryInput.RegisterActForStarted(_outGameCategoryInput.EnterActOnOutGameCategory, PushEnter);
                _outGameCategoryInput.RegisterActForStarted(_outGameCategoryInput.CancelActOnOutGameCategory, CloseCategory);
            }
        }

        #region Title
        void OpenCategory(InputAction.CallbackContext context)
        {
            _outGameFlow.OpenCategory();
        }

        void SelectNextCategory(InputAction.CallbackContext context)
        {
            _outGameFlow.SelectCategory(IndexMove.Next);
        }

        void SelectBackCategory(InputAction.CallbackContext context)
        {
            _outGameFlow.SelectCategory(IndexMove.Back);
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
            _outGameFlow.TitleSelectForKeyboard(int.Parse(key) - 1);
        }

        /// <summary>
        /// タイトル項目を選択する関数
        /// LRボタンに対応
        /// </summary>
        void CategorySelectNextForGamePad(InputAction.CallbackContext context)
        {
            _outGameFlow.TitleSelectForGamePad(IndexMove.Next);
        }

        /// <summary>
        /// タイトル項目を選択する関数
        /// LRボタンに対応
        /// </summary>
        void CategorySelectBackForGamePad(InputAction.CallbackContext context)
        {
            _outGameFlow.TitleSelectForGamePad(IndexMove.Back);
        }

        /// <summary>
        /// タイトル項目内のカテゴリーを切り替える関数
        /// Wキー、上矢印/スティック上、十字キー上に対応
        /// </summary>
        void TitleCategoryUp(InputAction.CallbackContext context)
        {
            _outGameFlow.TitleCategorySelect(IndexMove.Back);
        }

        /// <summary>
        /// タイトル項目内のカテゴリーを切り替える関数
        /// Sキー、下矢印/スティック下、十字キー下に対応
        /// </summary>
        void TitleCategoryDown(InputAction.CallbackContext context)
        {
            _outGameFlow.TitleCategorySelect(IndexMove.Next);
        }

        /// <summary>
        /// タイトル項目内のカテゴリーを切り替える関数
        /// Aキー、左矢印/スティック左、十字キー左に対応
        /// </summary>
        void TitleCategoryElementChangeLeft(InputAction.CallbackContext context)
        {
            _outGameFlow.TitleCategoryElementSelect(IndexMove.Back);
        }

        /// <summary>
        /// タイトル項目内のカテゴリーを切り替える関数
        /// Dキー、右矢印/スティック右、十字キー右に対応
        /// </summary>
        void TitleCategoryElementChangeRight(InputAction.CallbackContext context)
        {
            _outGameFlow.TitleCategoryElementSelect(IndexMove.Next);
        }

        /// <summary>
        /// エンター入力で呼ばれる関数
        /// </summary>
        void PushEnter(InputAction.CallbackContext context)
        {
            _outGameFlow.TitlePushEnter();
        }

        /// <summary>
        /// カテゴリーを閉じる関数
        /// </summary>
        /// <param name="context"></param>
        void CloseCategory(InputAction.CallbackContext context)
        {
            _outGameFlow.CloseCategory();
        }
        #endregion
    }
}
