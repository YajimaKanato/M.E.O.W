using UnityEngine;
using UnityEngine.InputSystem;

namespace DataDriven
{
    /// <summary>プレイヤーの入力処理を司るクラス</summary>
    public class PlayerMono : SceneEntity
    {
        PlaySceneFlow _playSceneFlow;
        PlaySceneInput _playSceneInput;
        UIFlow _uiFlow;
        UIInput _uiInput;
        MenuFlow _menuFlow;

        public override void Init()
        {
            tag = TagName.PLAYER;
            _playSceneFlow = FindFirstObjectByType<PlaySceneFlow>();
            _playSceneInput = FindFirstObjectByType<PlaySceneInput>();
            _uiFlow = FindFirstObjectByType<UIFlow>();
            _uiInput = FindFirstObjectByType<UIInput>();
            _menuFlow = FindFirstObjectByType<MenuFlow>();
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
            if (_playSceneInput)
            {
                //PlayScene
                _playSceneInput.RegisterActForStarted(_playSceneInput.ItemSlotActOnPlayScene, HotbarSelectForKeyboard);
                _playSceneInput.RegisterActForStarted(_playSceneInput.SlotNextActOnPlayScene, HotbarNextForGamePad);
                _playSceneInput.RegisterActForStarted(_playSceneInput.SlotBackActOnPlayScene, HotbarBackForGamePad);
                _playSceneInput.RegisterActForStarted(_playSceneInput.InteractActOnPlayScene, Interact);
                _playSceneInput.RegisterActForStarted(_playSceneInput.ItemActOnPlayScene, UseItem);
                _playSceneInput.RegisterActForStarted(_playSceneInput.DownActOnPlayScene, Down);
                _playSceneInput.RegisterActForStarted(_playSceneInput.JumpActOnPlayScene, Jump);
                _playSceneInput.RegisterActForStarted(_playSceneInput.RunActOnPlayScene, Run);
                _playSceneInput.RegisterActForCanceled(_playSceneInput.RunActOnPlayScene, Run);
                _playSceneInput.RegisterActForStarted(_playSceneInput.MenuActOnPlayScene, MenuOpen);
            }

            if (_uiInput)
            {
                //UI
                _uiInput.RegisterActForStarted(_uiInput.EnterActOnUI, Confirm);
                _uiInput.RegisterActForStarted(_uiInput.ItemSlotActOnUI, HotbarSelectOnConversationForKeyboard);
                _uiInput.RegisterActForStarted(_uiInput.SlotNextActOnUI, HotbarNextOnConversationForGamePad);
                _uiInput.RegisterActForStarted(_uiInput.SlotBackActOnUI, HotbarBackOnConversationForGamePad);
                _uiInput.RegisterActForStarted(_uiInput.MenuActOnUI, MenuOpen);
            }

            if (_playSceneInput && _uiInput)
            {
                Debug.Log("Registered");
            }
        }

        void ActionUnRegister()
        {
            if (_playSceneInput)
            {
                //PlayScene
                _playSceneInput.UnRegisterActForStarted(_playSceneInput.ItemSlotActOnPlayScene, HotbarSelectForKeyboard);
                _playSceneInput.UnRegisterActForStarted(_playSceneInput.SlotNextActOnPlayScene, HotbarNextForGamePad);
                _playSceneInput.UnRegisterActForStarted(_playSceneInput.SlotBackActOnPlayScene, HotbarBackForGamePad);
                _playSceneInput.UnRegisterActForStarted(_playSceneInput.InteractActOnPlayScene, Interact);
                _playSceneInput.UnRegisterActForStarted(_playSceneInput.ItemActOnPlayScene, UseItem);
                _playSceneInput.UnRegisterActForStarted(_playSceneInput.DownActOnPlayScene, Down);
                _playSceneInput.UnRegisterActForStarted(_playSceneInput.JumpActOnPlayScene, Jump);
                _playSceneInput.UnRegisterActForStarted(_playSceneInput.RunActOnPlayScene, Run);
                _playSceneInput.UnRegisterActForCanceled(_playSceneInput.RunActOnPlayScene, Run);
                _playSceneInput.UnRegisterActForStarted(_playSceneInput.MenuActOnPlayScene, MenuOpen);
            }

            if (_uiInput)
            {
                //UI
                _uiInput.UnRegisterActForStarted(_uiInput.EnterActOnUI, Confirm);
                _uiInput.UnRegisterActForStarted(_uiInput.ItemSlotActOnUI, HotbarSelectOnConversationForKeyboard);
                _uiInput.UnRegisterActForStarted(_uiInput.SlotNextActOnUI, HotbarNextOnConversationForGamePad);
                _uiInput.UnRegisterActForStarted(_uiInput.SlotBackActOnUI, HotbarBackOnConversationForGamePad);
                _uiInput.UnRegisterActForStarted(_uiInput.MenuActOnUI, MenuOpen);
            }

            if (_playSceneInput && _uiInput)
            {
                Debug.Log("UnRegistered");
            }
        }

        #region PlayScene
        #region Action
        private void Update()
        {
            if (!_playSceneFlow) return;
            _playSceneFlow.Move(_playSceneInput.MoveActOnPlayScene.ReadValue<Vector2>(), transform.position);
        }

        /// <summary>
        /// ジャンプする関数
        /// </summary>
        void Jump(InputAction.CallbackContext context)
        {
            _playSceneFlow.Jump();
        }

        /// <summary>
        /// 足場から降りる関数
        /// </summary>
        void Down(InputAction.CallbackContext context)
        {
            _playSceneFlow.Down(context.started);
        }

        /// <summary>
        /// 走る関数
        /// </summary>
        void Run(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                _playSceneFlow.Run(true);
            }
            else if (context.canceled)
            {
                _playSceneFlow.Run(false);
            }
        }
        #endregion

        /// <summary>
        /// アイテムを使用する関数
        /// Eキー/LTボタンに対応
        /// </summary>
        void UseItem(InputAction.CallbackContext context)
        {
            _playSceneFlow.UseItem();
        }

        /// <summary>
        /// ホットバーのスロットを選択する関数
        /// 1～6キーに対応
        /// </summary>
        void HotbarSelectForKeyboard(InputAction.CallbackContext context)
        {
            var key = context.control.name;
            if (key.Length > 1)
            {
                key = key.Substring(key.Length - 1);
            }
            Debug.Log(key);
            _playSceneFlow.HotbarSelectForKeyboard(int.Parse(key) - 1);
        }

        /// <summary>
        /// ホットバーのスロットを選択する関数
        /// LRボタンに対応
        /// </summary>
        void HotbarNextForGamePad(InputAction.CallbackContext context)
        {
            _playSceneFlow.HotbarselectForGamePad(IndexMove.Next);
        }

        /// <summary>
        /// ホットバーのスロットを選択する関数
        /// LRボタンに対応
        /// </summary>
        void HotbarBackForGamePad(InputAction.CallbackContext context)
        {
            _playSceneFlow.HotbarselectForGamePad(IndexMove.Back);
        }
        #endregion

        #region UI
        /// <summary>
        /// インタラクトを行う関数
        /// Tキー/Aボタンに対応
        /// </summary>
        void Interact(InputAction.CallbackContext context)
        {
            var id = _playSceneFlow.GetTarget(transform.position);
            if (_uiInput && id != default) _uiFlow.Interact(id);
        }

        /// <summary>
        /// 意思決定をする関数
        /// Eキー、エンターキー/Aボタンに対応
        /// </summary>
        void Confirm(InputAction.CallbackContext context)
        {
            _uiFlow.Confirm();
        }

        /// <summary>
        /// ホットバーのスロットを選択する関数
        /// 1～6キーに対応
        /// </summary>
        void HotbarSelectOnConversationForKeyboard(InputAction.CallbackContext context)
        {
            var key = context.control.name;
            if (key.Length > 1)
            {
                key = key.Substring(key.Length - 1);
            }
            Debug.Log(key);
            _uiFlow.HotbarSelectOnConversationForKeyboard(int.Parse(key) - 1);
        }

        /// <summary>
        /// ホットバーのスロットを選択する関数
        /// LRボタンに対応
        /// </summary>
        void HotbarNextOnConversationForGamePad(InputAction.CallbackContext context)
        {
            _uiFlow.HotbarselectOnConversationForGamePad(IndexMove.Next);
        }

        /// <summary>
        /// ホットバーのスロットを選択する関数
        /// LRボタンに対応
        /// </summary>
        void HotbarBackOnConversationForGamePad(InputAction.CallbackContext context)
        {
            _uiFlow.HotbarselectOnConversationForGamePad(IndexMove.Back);
        }
        #endregion

        /// <summary>
        /// メニューを開く関数
        /// </summary>
        void MenuOpen(InputAction.CallbackContext context)
        {
            _menuFlow.MenuOpen();
        }
    }
}
